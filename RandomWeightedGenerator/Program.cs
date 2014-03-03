using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Given a dictionary such as 
// { 'goog' => 200, 'amzn' => 300, 'msft' => 500}
// where the key is the name of the stock
// and the value is the number of times the stock was traded that day,
// design a random generator that picks a stock according to the 
// volume of stock that was traded
namespace RandomWeightedGenerator
{
    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, int> stockData = new Dictionary<string, int>();

            stockData.Add("amzn", 300);
            stockData.Add("msft", 500);
            stockData.Add("goog", 200);

            generateRandomStock(stockData);
        }

        static void generateRandomStock(Dictionary<string, int> stockData)
        {
            // The values in the stockData represents
            // the volume of the stock being traded
            // This needs to be treated as a weight
            // so that it can be used with the random
            // generator. 

            int totalWeight = 0;

            foreach (var entry in stockData)
            {
                totalWeight += entry.Value;
            }

            Dictionary<string, float> entries = new Dictionary<string, float>();

            foreach (var entry in stockData)
            {
                entries.Add(entry.Key, (float)entry.Value / totalWeight);
            }

            // Sort the stockData according to value
            KeyValuePair<string, float>[] entriesArray = entries.ToArray();

            Array.Sort(entriesArray, customSort);

            Dictionary<string, float> sortedEntries = new Dictionary<string, float>();

            foreach (var entry in entriesArray)
            {
                sortedEntries.Add(entry.Key, entry.Value);
            }

            float cumulativeSum = 0;
            Dictionary<string, float> cumulativeSortedEntries = new Dictionary<string, float>();

            foreach (var entry in sortedEntries)
            {
                cumulativeSum += entry.Value;

                cumulativeSortedEntries.Add(entry.Key, cumulativeSum);
            }

            Random floatRandomGen = new Random();
            double randDouble = floatRandomGen.NextDouble();

            foreach (var entry in cumulativeSortedEntries)
            {
                if (randDouble < entry.Value)
                {
                    Console.WriteLine(entry.Key);
                    return;
                }
            }

        }

        static int customSort(KeyValuePair<string, float> obj1, KeyValuePair<string, float> obj2)
        {
            return obj1.Value.CompareTo(obj2.Value);
        }
    }
}

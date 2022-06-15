/*
 * Program:         Question 6
 * File:            Program.cs
 * Course:          INFO-3138
 * Date:            May 26, 2021
 * Description:     A sample solution to question 6 of the midterm test review.
 */

using System;
using System.Collections.Generic;   // IEnumerable<> type
using Newtonsoft.Json;              // JsonException class
using Newtonsoft.Json.Linq;         // JObject class
using System.IO;                    // File class
using System.Text;                  // StringBuilder class

namespace Question_6
{
    class Program
    {
        // Constants
        private const string JSON_FILE = "nutrition.json";
        private const int MAX_LINE_LENGTH = 80;
        private static readonly string[] PROPERTY_NAMES =  { "total-fat", "saturated-fat", 
            "cholesterol", "sodium", "carbohydrates", "fiber", "protein" };
        private static readonly string[] PROPERTY_UNITS = { "g", "g", "mg", "mg", "g", "g", "g" };

        static void Main(string[] args)
        {
            // Display a title
            Console.WriteLine("Food Query\n");

            try
            {
                // Load the nutrition data into an object
                JObject jsonObject = JObject.Parse(File.ReadAllText(JSON_FILE));

                // Display a menu of the property names 
                for (int i = 0; i < PROPERTY_NAMES.Length; ++i)
                    Console.WriteLine($"{i+1}. {PROPERTY_NAMES[i]}");

                // Get the user to select a property 
                int itemNum;
                bool valid;
                do
                {
                    Console.Write("\nEnter the number of a substance from the above list: ");
                    valid = Int32.TryParse(Console.ReadLine(), out itemNum) && itemNum > 0 && itemNum <= PROPERTY_NAMES.Length;
                    if (!valid)
                        Console.WriteLine("ERROR: Not a valid entry. Please try again.");
                } while (!valid);

                // Get the user to enter an amount of the selected subtance
                double amount;
                do
                {
                    Console.Write($"\nEnter a number of {PROPERTY_UNITS[itemNum-1]} to use as a threshold amount in the queries: ");
                    valid = Double.TryParse(Console.ReadLine(), out amount) && amount >= 0;
                    if (!valid)
                        Console.WriteLine("ERROR: Not a valid entry. Please try again.");
                } while (!valid);

                // Perform a query to report foods that are below the threshold
                Console.WriteLine($"\nThe following foods have less than {amount} {PROPERTY_UNITS[itemNum-1]} of {PROPERTY_NAMES[itemNum-1]} per serving...");
                string query1 = $"$.food[?(@.{PROPERTY_NAMES[itemNum-1]} < {amount})].name";
                Console.WriteLine(getQueryResults(jsonObject, query1));
                Console.WriteLine($"The following foods have more than {amount} {PROPERTY_UNITS[itemNum - 1]} of {PROPERTY_NAMES[itemNum - 1]} per serving...");
                string query2 = $"$.food[?(@.{PROPERTY_NAMES[itemNum - 1]} > {amount})].name";
                Console.WriteLine(getQueryResults(jsonObject, query2));

            }
            catch (IOException ex)
            {
                Console.WriteLine($"FILE ERROR: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON ERROR: {ex.Message}");
            }

        } // end Main()

        private static string getQueryResults(JObject obj, string query)
        {
            IEnumerable<JToken> results = obj.SelectTokens(query);
            StringBuilder sb = new StringBuilder();
            
            foreach (JToken r in results)
                sb.Append(r.ToString() + "\n");

            return sb.ToString();

        } // end getQueryResults()

    } // end class Program 
}

/*
 * Program:         Question 3
 * File:            Program.cs
 * Course:          INFO-3138
 * Date:            May 26, 2021
 * Description:     A sample solution to question 3 of the midterm test review.
 */ 

using System;

using System.Collections.Generic;   // List<> class
using Newtonsoft.Json;              // JsonConvert class
using Newtonsoft.Json.Schema;       // JSchema class
using Newtonsoft.Json.Linq;         // JObject class
using System.IO;                    // File class

namespace Question_3
{
    class Program
    {
        // Member constants
        private const string DATA_FILE = "trivia-data.json";
        private const string SCHEMA_FILE = "trivia-schema.json";

        static void Main(string[] args)
        {
            try
            {
                // Obtain a collection of the trivia questions as a List<Question> 
                List<Question> allQuestions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(DATA_FILE));

                // Display all the questions
                Console.WriteLine("Here are the stored trivia questions...\n");
                int n = 0;
                foreach (Question q in allQuestions)
                {
                    Console.WriteLine($"{++n}.\t{q.Text}");
                    for (int i = 0; i < q.Choices.Count; ++i)
                    {
                        Console.Write($"\t{(char)('A' + i)}.\t{q.Choices[i]}");
                        if (i == q.CorrectIndex)
                            Console.Write("  [CORRECT]");
                        Console.WriteLine();
                    }
                }

                // Read the schema file to a JSON string
                string json_schema = File.ReadAllText(SCHEMA_FILE);
                
                // Add a new, validated question
                Console.WriteLine("\nPlease add one more question along with at least four choices...");
                bool valid;
                Question qNew = null;
                do
                {
                    // Creating a new Question object each time ensures there isn't old information 
                    // a previous iteration of this validation loop
                    qNew = new Question();

                    Console.Write("\nQuestion text: ");
                    qNew.Text = Console.ReadLine();
                    int i = 0;
                    bool done;
                    do
                    {
                        Console.Write($"Choice {++i} (or press enter to finish): ");
                        string choice = Console.ReadLine();
                        done = choice.Trim() == "";
                        if (!done)
                            qNew.Choices.Add(choice);
                    } while (!done);
                    do
                    {
                        Console.Write("Number of correct answer: ");
                        int ansNum;
                        valid = Int32.TryParse(Console.ReadLine(), out ansNum);
                        if (valid)
                            qNew.CorrectIndex = ansNum - 1;
                        else
                            Console.WriteLine("ERROR: Number of correct answer must be an integer value.");
                    } while (!valid);

                    IList<string> errorMessages;
                    valid = ValidQuestion(qNew, json_schema, out errorMessages);

                    if (!valid)
                    {
                        Console.WriteLine("ERROR: The question data is invalid. Try entering it again.");
                        foreach (string message in errorMessages)
                            Console.WriteLine(message);
                    }

                } while (!valid);
                allQuestions.Add(qNew);
                Console.WriteLine("\nYour new question has been added.");

                // Save the questions to the file
                string json = JsonConvert.SerializeObject(allQuestions);
                File.WriteAllText(DATA_FILE, json);
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine($"ERROR: Problem accessing the file '{DATA_FILE}'.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}.");
            }
        }

        // Accepts a Question object and a JSON schema string and returns true if the Question is 
        // validated by the schema, otherwise returns false and populates the output parameter
        // messages with validation error information
        private static bool ValidQuestion(Question question, string json_schema, out IList<string> messages)
        {
            // Convert item object to a JSON string 
            string json_data = JsonConvert.SerializeObject(question);

            // Validate the data string against the schema contained in the 
            // json_schema parameter
            JSchema schema = JSchema.Parse(json_schema);
            JObject qObj = JObject.Parse(json_data);
            return qObj.IsValid(schema, out messages);

        } // end ValidateQuestion()

    } // end class Program

    // A class definition for each trivia question in the JSON file
    class Question
    {
        public string Text { get; set; }
        public List<string> Choices = new List<string>();
        public int CorrectIndex { get; set; }
    } // end class Question
}

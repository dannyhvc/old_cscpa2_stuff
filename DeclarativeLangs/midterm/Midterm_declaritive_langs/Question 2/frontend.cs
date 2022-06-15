using Newtonsoft.Json;
using Question_2.backend;
using System;
using System.IO;

namespace Question_2.frontend
{
    public class RecipeConsole
    {
        public RecipeManager recipeManager { get; set; }
        string title { get; set; }
        string name;
        int amount;
        string units;

        public RecipeConsole()
        {
            Console.WriteLine("Recipe Ingredients\n");
            ScanTitle();
            recipeManager = new RecipeManager(title!);
        }

        void ScanTitle()
        {
            Console.Write("Enter a Recipe title: ");
            title = Console.ReadLine();
        }

        void ScanIngredientDescription()
        {
            Console.Write("Enter ingredient or press <enter> to quit: ");
            name = Console.ReadLine();

            if (name == "")
            {
                SaveRecipe();
                Environment.Exit(0);
            }
        }

        void ScanIngredientAmount()
        {
            Console.Write($"Enter the amount of {name}'s: ");
            try
            {
                amount = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.Write("the input is not a number");
            }
        }

        void ScanIngredientUnits()
        {
            Console.Write($"Enter the units for {name}'s: ");
            units = Console.ReadLine();
        }

        void Add()
            => recipeManager.AddIngredient(
                new Ingredient { Description = name, Amount = amount, Units = units });

        void SaveRecipe()
        {
            string output = JsonConvert.SerializeObject(recipeManager.Recipe);
            File.WriteAllText("question_2.json", output);
            Console.WriteLine("Your Recipe ingredients have been saved.");
        }

        public void Run()
        {
            while (true)
            {
                ScanIngredientDescription();
                ScanIngredientAmount();
                ScanIngredientUnits();
                Add();
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace BadCarFactory.Phase1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CarFactory carFactory = new CarFactory();
            List<ICar> carList = new List<ICar>();

            Console.WriteLine("Welcome to the car factory");
            Console.WriteLine("What kind of car would you like to make?\n");

            string userInput;
            bool keepMakingCars = true;

            do
            {
                // get user input
                Console.Write("Please enter the type of car you would like to make. To quit, type 'Exit': ");
                userInput = Console.ReadLine();

                // examine user input
                switch (userInput)
                {
                    case "Sedan":
                        carList.Add(carFactory.MakeSedan());
                        break;
                    case "Truck":
                        carList.Add(carFactory.MakeTruck());
                        break;
                    case "Coupe":
                        carList.Add(carFactory.MakeCoupe());
                        break;
                    case "Exit":
                        keepMakingCars = false;
                        continue;
                    default:
                        Console.WriteLine($"Sorry, '{userInput}' is not a type of car that we make\n");
                        continue;
                }

                Console.WriteLine($"Made a new {userInput}");
                Console.WriteLine($"You've made {carList.Count} new cars\n");
            } while (keepMakingCars);

            Console.WriteLine("\nCars Made:");
            foreach (var car in carList)
            {
                Console.WriteLine(car.GetCarType());
            }

            Console.WriteLine("\nGoodbye\n");
        }
    }
}

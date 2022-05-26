using System;
using System.Collections.Generic;

namespace CarFactoryMethod.Phase1
{
    class Program
    {
        public static void Main(string[] args)
        {
            CarFactory carFactory = new CarFactory();
            List<ICar> carList = new List<ICar>();

            // ask a user to create some cars
            Console.WriteLine("Welcome to the car factory");
            Console.WriteLine("What kind of car would you like to make?\n");

            string userInput;
            bool keepMakingCars = true;

            while (keepMakingCars)
            {
                // get user input
                Console.Write("Please enter the type of car you would like to make. To quit, type 'Exit': ");
                userInput = Console.ReadLine();

                // check if they wanted to quit
                if(userInput == "Exit")
                {
                    keepMakingCars = false;
                    continue;
                }

                // try and create that car
                var newCar = carFactory.MakeCar(userInput);

                // if the car is invalid, try again
                if (newCar == null)
                {
                    Console.WriteLine($"Sorry, '{userInput}' is not a type of car that we make\n");
                    continue;
                }

                // if the car is valid, add it to the list
                carList.Add(newCar);
                Console.WriteLine($"Made a new {userInput}");
                Console.WriteLine($"You've made {carList.Count} new cars\n");
            }

            Console.WriteLine("\nCars Made:");
            foreach (var car in carList)
            {
                Console.WriteLine(car.GetCarType());
            }

            Console.WriteLine("\nGoodbye\n");
        }
    }
}

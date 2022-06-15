using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var director = new Director();

            var vehicles = new List<Vehicle>();
            vehicles.Add(director.BuildVehicle(new SedanBuilder()));
            vehicles.Add(director.BuildVehicle(new TricycleBuilder()));
            vehicles.Add(director.BuildVehicle(new SevenFortySevenBuilder()));

            foreach(var vehicle in vehicles)
            {
                Console.WriteLine($"Type:    {vehicle.Type}");
                Console.WriteLine($"Engine:  {vehicle.EngineType}");
                Console.WriteLine($"Seats:   {vehicle.NumberOfSeats}");
                Console.WriteLine($"Doors:   {vehicle.NumberOfDoors}");
                Console.WriteLine($"Wheels:  {vehicle.NumberOfWheels}");
                Console.WriteLine();
            }
        }
    }
}

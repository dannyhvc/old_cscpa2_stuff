using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuilder
{
    public class SevenFortySevenBuilder : IVehicleBuilder
    {
        private Vehicle _vehicle;
        public void BuildDoors()
        {
            // this number is from counting the instances of the word "door" in a description of a 747 model on wikipedia. 
            // it's totally possible that it's inaccurate
            _vehicle.NumberOfDoors = 4;
        }

        public void BuildEngine()
        {
            _vehicle.EngineType = "4 Jet Engines";
        }

        public void BuildSeats()
        {
            _vehicle.NumberOfSeats = 416;
        }

        public void BuildVehicle()
        {
            _vehicle = new Vehicle();
            _vehicle.Type = "747";
        }

        public void BuildWheels()
        {
            _vehicle.NumberOfWheels = 16;
        }

        public Vehicle GetVehicle()
        {
            return _vehicle;
        }
    }
}

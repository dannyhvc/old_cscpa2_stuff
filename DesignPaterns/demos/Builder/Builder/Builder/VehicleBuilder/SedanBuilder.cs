using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuilder
{
    public class SedanBuilder : IVehicleBuilder
    {
        private Vehicle _vehicle;
        public void BuildDoors()
        {
            _vehicle.NumberOfDoors = 4;
        }

        public void BuildEngine()
        {
            _vehicle.EngineType = "4 Cylinder";
        }

        public void BuildSeats()
        {
            _vehicle.NumberOfSeats = 5;
        }

        public void BuildVehicle()
        {
            _vehicle = new Vehicle();
            _vehicle.Type = "Sedan";
        }

        public void BuildWheels()
        {
            _vehicle.NumberOfWheels = 4;
        }

        public Vehicle GetVehicle()
        {
            return _vehicle;
        }
    }
}

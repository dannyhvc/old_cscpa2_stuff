using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuilder
{
    public class TricycleBuilder : IVehicleBuilder
    {
        private Vehicle _vehicle;

        public void BuildDoors()
        {
            _vehicle.NumberOfDoors = 0;
        }

        public void BuildEngine()
        {
            _vehicle.EngineType = "Pedals";
        }

        public void BuildSeats()
        {
            _vehicle.NumberOfSeats = 1;
        }

        public void BuildVehicle()
        {
            _vehicle = new Vehicle();
            _vehicle.Type = "Tricycle";
        }

        public void BuildWheels()
        {
            _vehicle.NumberOfWheels = 3;
        }

        public Vehicle GetVehicle()
        {
            return _vehicle;
        }
    }
}

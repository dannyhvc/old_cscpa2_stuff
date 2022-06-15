using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBuilder
{

    public interface IVehicleBuilder
    {
        void BuildVehicle();
        void BuildWheels();
        void BuildDoors();
        void BuildSeats();
        void BuildEngine();
        Vehicle GetVehicle();
        
    }
}

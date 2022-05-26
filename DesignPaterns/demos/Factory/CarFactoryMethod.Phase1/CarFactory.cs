using System;
using System.Collections.Generic;
using System.Text;

namespace CarFactoryMethod.Phase1
{
    public class CarFactory
    {
        // This is the actual factory method
        // declared virtual to allow for overwriting in an inherited class later on
        public virtual ICar MakeCar(string type = null)
        {
            switch (type)
            {
                case "Sedan":
                    return new Sedan();
                case "Coupe":
                    return new Coupe();
                case "Truck":
                    return new Truck();
                default:
                    // no passed parameter or any non-known parameter returns null
                    return null;
            }
        }
    }
}

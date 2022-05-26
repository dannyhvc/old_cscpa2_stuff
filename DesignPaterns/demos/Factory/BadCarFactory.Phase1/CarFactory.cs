using System;
using System.Collections.Generic;
using System.Text;

namespace BadCarFactory.Phase1
{
    /// <summary>
    /// Simple class to make different kinds of cars
    /// </summary>
    public class CarFactory
    {
        public ICar MakeTruck()
        {
            return new Truck();
        }

        public ICar MakeSedan()
        {
            return new Sedan();
        }
        
        public ICar MakeCoupe()
        {
            return new Coupe();
        }
    }
}

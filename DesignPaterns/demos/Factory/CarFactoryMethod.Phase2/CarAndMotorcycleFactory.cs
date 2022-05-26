using System;
using System.Collections.Generic;
using System.Text;

namespace CarFactoryMethod.Phase2
{
    public class Motorcycle : ICar
    {
        public string GetCarType ()
        {
            return "Motorcycle";
        }
    }
    
    /// <summary>
    /// AN example to demo extending the car factory.
    /// This is useful if you're using someone else's 
    /// library that defines a factory, but you need 
    /// additional types to be created.
    /// </summary>
    public class CarAndMotorcycleFactory : CarFactory
    {
        public override ICar MakeCar(string type = null)
        {
            if (type == "Motorcycle")
            {
                return new Motorcycle();
            }

            return base.MakeCar(type);
        }
    }
}

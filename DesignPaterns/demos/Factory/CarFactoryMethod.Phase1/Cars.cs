using System;
using System.Collections.Generic;
using System.Text;

namespace CarFactoryMethod.Phase1
{
    /// <summary>
    /// Defines the interface all cars must conform to
    /// </summary>
    public interface ICar
    {
        string GetCarType();
    }

    public class Sedan : ICar
    {
        public string GetCarType()
        {
            return "Sedan";
        }
    }

    public class Truck : ICar
    {
        public string GetCarType()
        {
            return "Truck";
        }
    }

    public class Coupe : ICar
    {
        public string GetCarType()
        {
            return "Coupe";
        }
    }
}

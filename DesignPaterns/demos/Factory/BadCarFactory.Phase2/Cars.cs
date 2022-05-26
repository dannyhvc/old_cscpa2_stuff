using System;
using System.Collections.Generic;
using System.Text;

namespace BadCarFactory.Phase2
{
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

    // added a new type of car
    public class Wagon : ICar
    {
        public string GetCarType()
        {
            return "Wagon";
        }
    }
}

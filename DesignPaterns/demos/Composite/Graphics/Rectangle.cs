using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class Rectangle : Graphic
    {
        private int _width;
        private int _height;

        public Rectangle(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public override void Draw()
        {
            for(int yIndex = 0; yIndex < _height; yIndex++)
            {
                for (int xIndex = 0; xIndex < _width; xIndex++)
                {
                    Console.Write("■");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

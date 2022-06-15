using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class Line : Graphic
    {
        private int _length;

        public Line(int length)
        {
            _length = length;
        }

        public override void Draw()
        {
            for (int index = 0; index < _length; index++)
            {
                Console.Write("_");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

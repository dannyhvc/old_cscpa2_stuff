using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class Text : Graphic
    {
        private string _text;

        public Text(string text)
        {
            _text = text;
        }
        public override void Draw()
        {
            Console.WriteLine(_text);
            Console.WriteLine();
        }
    }
}

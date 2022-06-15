using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    class Program
    {
        static void Main(string[] args)
        {
            var graphicsToDraw = new List<Graphic>();

            graphicsToDraw.Add(new Line(15));
            graphicsToDraw.Add(new Text("This is a simple Text element"));
            graphicsToDraw.Add(new Rectangle(10, 3));

            var childGraphic = new Picture("Child Graphic");
            childGraphic.Add(new Line(18));
            childGraphic.Add(new Text("This text is the child of the child graphic"));
            childGraphic.Add(new Rectangle(5, 5));

            var parentGraphic = new Picture("Parent Graphic");
            parentGraphic.Add(new Line(20));
            parentGraphic.Add(childGraphic);
            parentGraphic.Add(new Rectangle(20, 7));
            parentGraphic.Add(new Text("This text is the child of the parent graphic"));

            graphicsToDraw.Add(parentGraphic);

            foreach (var graphic in graphicsToDraw)
            {
                graphic.Draw();
            }

        }
    }
}

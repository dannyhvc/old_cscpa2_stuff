using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class Picture : Graphic
    {
        private List<Graphic> _children = new List<Graphic>();
        private string _name;

        public Picture(string name)
        {
            _name = name;
        }
        public override void Draw()
        {
            Console.WriteLine($"Begin Picture '{_name}'");
            foreach(var child in _children)
            {
                child.Draw();
            }
            Console.WriteLine($"End Picture '{_name}'");
            Console.WriteLine();
        }

        public override void Add(Graphic graphic)
        {
            if (!_children.Contains(graphic))
            {
                _children.Add(graphic);
            }
        }

        public override void Remove(Graphic graphic)
        {
            _children.Remove(graphic);
        }

        public override Graphic GetChild(int child)
        {
            return base.GetChild(child);
        }
    }
}

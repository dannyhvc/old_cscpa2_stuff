using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public abstract class Graphic
    {
        public abstract void Draw();
        public virtual void Add(Graphic graphic) { } // empty default implementation
        public virtual void Remove(Graphic graphic) { } // empty default implementation
        public virtual Graphic GetChild(int child) { return null; } // empty default implementation
    }
}

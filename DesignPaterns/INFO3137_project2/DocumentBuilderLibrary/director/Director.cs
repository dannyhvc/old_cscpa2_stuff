using DocumentBuilderLibrary.builders;
using DocumentBuilderLibrary.patterns;

namespace DocumentBuilderLibrary.director
{
    public class Director : IDirector
    {
        public IBuilder Builder { get; private set; }
        public string? Name { private get; set; }
        public string? Content { private get; set; }

        public Director(string mode)
        {
            // dispatching for builder creation
            switch (mode.ToUpper())
            {
                case "JSON":
                    Builder = new JsonBuilder();
                    break;
                case "XML":
                    Builder = new XmlBuilder();
                    break;
                default:
                    throw new ArgumentException("Invalid input. For Usage, type 'Help'"); //TODO
            }
        }

        public void BuildBranch() => Builder.BuildBranch(Name!);

        public void BuildLeaf() => Builder.BuildLeaf(Name!, Content!);

        public void CloseBranch() => Builder.CloseBranch();
    }
}

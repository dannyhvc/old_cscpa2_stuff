using DocumentBuilderLibrary.branches;
using DocumentBuilderLibrary.leafs;
using DocumentBuilderLibrary.patterns;

namespace DocumentBuilderLibrary.builders
{
    public class XmlBuilder : IBuilder
    {
        IComposite _root = new XmlBranch("root");
        Stack<IComposite> _last_opened = new();

        public XmlBuilder() => _last_opened.Push(_root);

        public void BuildBranch(string name)
        {
            var branch = new XmlBranch(name);
            _last_opened.Peek().AddChild(branch);
            _last_opened.Push(branch);
        }

        public void BuildLeaf(string name, string content) => _last_opened.Peek().AddChild(new XmlLeaf(name, content));

        public void CloseBranch()
        {
            // making sure we don't pop the root node
            if (_last_opened.Count > 1)
                _last_opened.Pop();
        }

        public IComposite GetDocument() => _root;
    }
}

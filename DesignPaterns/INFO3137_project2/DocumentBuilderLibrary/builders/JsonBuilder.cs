using DocumentBuilderLibrary.branches;
using DocumentBuilderLibrary.leafs;
using DocumentBuilderLibrary.patterns;

namespace DocumentBuilderLibrary.builders
{
    public class JsonBuilder : IBuilder
    {
        readonly IComposite _root = new JsonBranch("root");
        readonly Stack<IComposite> _last_opened = new();

        public JsonBuilder() => _last_opened.Push(_root);

        public void BuildBranch(string name)
        {
            var branch = new JsonBranch(name);
            _last_opened.Peek().AddChild(branch);
            _last_opened.Push(branch);
        }

        public void BuildLeaf(string name, string content) => _last_opened.Peek().AddChild(new JsonLeaf(name, content));

        public void CloseBranch()
        {
            if (_last_opened.Count > 1)
                _last_opened.Pop();
        }

        public IComposite GetDocument() => _root;
    }
}

using DocumentBuilderLibrary.patterns;
using System.Text;

namespace DocumentBuilderLibrary.leafs
{
    public class XmlLeaf : IComposite
    {

        string _name;
        string _content;

        public XmlLeaf(string name, string content)
        {
            _name = name;
            _content = content;
        }

        public void AddChild(IComposite child) { /*nothing happens here*/ }

        public string Print(int depth)
        {
            StringBuilder indent = new();
            for (int i = 0; i < depth; i++) indent.Append('\t');
            return $"{indent}<{_name}>{_content}</{_name}>";
        }
    }
}

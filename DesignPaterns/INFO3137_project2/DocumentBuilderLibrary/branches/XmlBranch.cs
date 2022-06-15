using DocumentBuilderLibrary.patterns;
using System.Text;

namespace DocumentBuilderLibrary.branches
{
    public class XmlBranch : IComposite
    {
        private string _name;
        private List<IComposite> _content = new List<IComposite>();

        public XmlBranch(string name) => _name = name;

        public void AddChild(IComposite child) => _content.Add(child);

        public string Print(int depth)
        {
            // defs
            StringBuilder sb = new();
            StringBuilder indent = new();
            string start;
            string end;

            for (int i = 0; i < depth; i++) indent.Append('\t');

            // checking wether we're in the root node or not
            if (depth > 0)
            {
                start = $"{indent}<{_name}>\n";
                end = $"{indent}</{_name}>";
            }
            else
            {
                start = $"<{_name}>\n";
                end = $"</{_name}>";
            }

            // making the indents

            // build branch string
            sb.Append(start);
            foreach (IComposite child in _content) sb.Append($"{child.Print(depth + 1)}\n");
            sb.Append(end);

            return sb.ToString();
        }
    }
}

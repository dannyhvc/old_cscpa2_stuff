using DocumentBuilderLibrary.patterns;
using System.Text;

namespace DocumentBuilderLibrary.branches
{
    public class JsonBranch : IComposite
    {
        private string _name;
        private List<IComposite> _content = new List<IComposite>();

        public JsonBranch(string name) => _name = name;

        public void AddChild(IComposite child) => _content.Add(child);

        public string Print(int depth)
        {
            // let t_len = tabs * depth
            // tab the name and brace by t_len
            // for each child print it by tab t_len + 1
            // tab closing brace by t_len

            StringBuilder indent = new();
            StringBuilder sb = new();
            string start;
            string end;

            for (int i = 0; i < depth; i++) indent.Append('\t');

            if (depth > 0)
            {
                start = $"{indent}\"{_name}\" : {{\n";
                end = $"{indent}}}";
            }
            else
            {
                start = "{\n";
                end = "}";
            }

            // create string to print
            sb.Append(start);
            foreach (var it in _content.Select((Child, Index) => new { Child, Index }))
            {
                if (it.Index != _content.Count - 1)
                    sb.Append($"{it.Child.Print(depth + 1)},\n");
                else
                    sb.Append($"{it.Child.Print(depth + 1)}\n");
            }
            sb.Append(end);

            return sb.ToString();
        }
    }
}

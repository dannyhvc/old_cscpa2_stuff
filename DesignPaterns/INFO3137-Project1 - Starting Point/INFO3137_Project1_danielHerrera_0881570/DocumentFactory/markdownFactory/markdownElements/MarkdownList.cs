using System;
using System.Linq;
using System.Text;

namespace DocumentFactory.markdownFactory.markdownElements
{
    public class MarkdownList : IElement
    {
        readonly string _list_type;
        readonly string[] _list_items;

        public MarkdownList(string props)
        {
            if (props == null)
                throw new ArgumentNullException("Props passed are null in List element");

            // splitting the props into usable list of props
            var props_list = props.Split(';');

            // making sure that the only entries are the header size and the text in the header
            if (props_list.Length < 1)
                throw new ArgumentException("Not enough props for List element");

            const int LIST_TYPE_IDX = 0;
            _list_type = props_list[LIST_TYPE_IDX];

            // Getting the rest of the list not including the list type
            _list_items = new string[props_list.Length - 1];
            for (int i = 0; i < _list_items.Length; i++)
                _list_items[i] = props_list[i + 1];
        }

        public override string ToString()
        {
            StringBuilder md = new StringBuilder();

            // setting the parent tag type for the list
            if (_list_type == "Ordered")
                foreach (var it in _list_items.Select((Value, Index) => new { Value, Order = Index + 1 }))
                    md.Append($"{it.Order}. {it.Value}\n");

            else if (_list_type == "Unordered")
                foreach (var item in _list_items)
                    md.Append($"- {item}\n");
            else
                throw new ArgumentException($"The value {_list_type} is not a valid argument");

            // returning the string or parent tags wraps around the li items in the buffer
            return md.ToString();
        }
    }
}

using System;
using System.Text;

namespace DocumentFactory.htmlFactory.htmlElements
{
    public class HTMLList : IElement
    {
        string _list_type;
        string[] _list_items;

        public HTMLList(string props)
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
            string listTypeTag = "";
            StringBuilder listItemsTag = new StringBuilder();

            // setting the parent tag type for the list
            if (_list_type == "Ordered")
                listTypeTag = "ol";
            else if (_list_type == "Unordered")
                listTypeTag = "ul";

            // adding the li elements with the string items into a string buffer
            foreach (string item in _list_items)
                listItemsTag.Append($"<li>{item}</li>");

            // returning the string or parent tags wraps around the li items in the buffer
            return $"<{listTypeTag}>{listItemsTag.ToString()}</{listTypeTag}>";
        }
    }
}

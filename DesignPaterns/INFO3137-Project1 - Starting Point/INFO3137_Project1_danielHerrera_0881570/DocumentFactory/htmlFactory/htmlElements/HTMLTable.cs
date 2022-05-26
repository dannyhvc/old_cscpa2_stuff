using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentFactory.htmlFactory.htmlElements
{
    public class HTMLTable : IElement
    {
        List<List<string>> _table;

        public HTMLTable(string props)
        {
            _table = new List<List<string>>();
            if (props == null)
                throw new ArgumentNullException("Props passed are null in HTMLTable element");

            // splitting the props into usable list of list of text in row x col form
            foreach (string row in props.Split(';'))
            {
                var rows_list = new List<string>();
                foreach (string col in row.Split('$'))
                {
                    rows_list.Add(col);
                }
                _table.Add(rows_list);
            }
        }

        public override string ToString()
        {
            StringBuilder html = new StringBuilder();
            const int HEAD_IDX = 0;

            // table head string colapsing
            html.Append("<thead>");
            html.Append("<tr>");
            foreach (string col in _table[HEAD_IDX]) html.Append($"<th>{col}</th>");
            html.Append("</tr>");
            html.Append("</thead>");

            // table body string colapsing
            html.Append("<thead>");
            for (int i = 1; i < _table.Count; i++)
            {
                html.Append("<tr>");
                foreach (var col in _table[i]) html.Append($"<td>{col}</td>");
                html.Append("</tr>");
            }
            html.Append("</thead>");

            // full table layout in string form
            return $"<table>{html}</table>";
        }
    }
}

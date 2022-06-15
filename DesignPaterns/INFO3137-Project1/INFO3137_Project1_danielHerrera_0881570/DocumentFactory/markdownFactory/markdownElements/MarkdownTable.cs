using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentFactory.markdownFactory.markdownElements
{
    public class MarkdownTable : IElement
    {

        List<List<string>> _table;

        public MarkdownTable(string props)
        {
            _table = new List<List<string>>();
            if (props == null)
                throw new ArgumentNullException("Props passed are null in Table element");

            // splitting the props into usable list of props
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

        private string createMarkdownRow(List<string> row, (string start, string end) pipes)
        {
            StringBuilder markdown_row = new StringBuilder();

            markdown_row.Append(pipes.start);
            for (int col_idx = 0; col_idx < row.Count; col_idx++)
                if (col_idx != row.Count - 1)
                    markdown_row.Append($"{row[col_idx]} {pipes.start}");
                else
                    markdown_row.Append($"{row[col_idx]} {pipes.end}");
            return markdown_row.ToString();
        }

        public override string ToString()
        {
            StringBuilder markdown_table = new StringBuilder();
            const string START_PIPE = "| ";
            const string END_PIPE = "|\n";
            const string HEADER_SEP = "---";
            const int HEAD_IDX = 0;

            // table head string colapsing
            markdown_table.Append(createMarkdownRow(_table[HEAD_IDX], (START_PIPE, END_PIPE)));

            // create seperators
            List<string> seperators = Enumerable.Repeat(HEADER_SEP, _table[HEAD_IDX].Count).ToList();
            markdown_table.Append(createMarkdownRow(seperators, (START_PIPE, END_PIPE)));

            for (int body_row_idx = 1; body_row_idx < _table.Count; body_row_idx++)
                markdown_table.Append(createMarkdownRow(_table[body_row_idx], (START_PIPE, END_PIPE)));

            // full table layout in string form
            return markdown_table.ToString();
        }
    }
}

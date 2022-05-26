using System;
using System.Text;

namespace DocumentFactory.markdownFactory.markdownElements
{
    public class MarkdownHeader : IElement
    {
        int _hsize;
        string _text;

        public MarkdownHeader(string props)
        {
            const int HEADER_SIZE_POS = 0;
            const int TEXT_POS = 1;

            if (props == null)
                throw new ArgumentNullException("Props passed are null in MarkdownHeader element");

            // splitting the props into usable list of props
            var props_list = props.Split(';');

            // making sure that the only entries are the header size and the text in the header
            if (props_list.Length > 2)
                throw new ArgumentException("Too many props for MarkdownHeader element");

            _hsize = Convert.ToInt32(props_list[HEADER_SIZE_POS]);
            _text = props_list[TEXT_POS];
        }

        public override string ToString()
        {
            StringBuilder md = new StringBuilder();
            for (int i = 1; i <= _hsize; i++)
                md.Append("#");
            md.Append(' ' + _text + '\n');
            return md.ToString();
        }
    }
}

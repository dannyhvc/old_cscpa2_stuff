using System;

namespace DocumentFactory.htmlFactory.htmlElements
{
    public class HTMLHeader : IElement
    {
        string _hsize;
        string _text;

        public HTMLHeader(string props)
        {
            // constants for indexing props list
            const int HEADER_SIZE_POS = 0;
            const int TEXT_POS = 1;

            if (props == null)
                throw new ArgumentNullException("Props passed are null in HTMLHeader element");

            // splitting the props into usable list of props
            var props_list = props.Split(';');

            // making sure that the only entries are the header size and the text in the header
            if (props_list.Length > 2)
                throw new ArgumentException("Too many props for HTMLHeader element");

            // setting state of current element until ToString is called
            _hsize = props_list[HEADER_SIZE_POS];
            _text = props_list[TEXT_POS];
        }

        public override string ToString() //overrides objet class ToString
        {
            return $"<h{_hsize}>{_text}</h{_hsize}>";
        }
    }
}

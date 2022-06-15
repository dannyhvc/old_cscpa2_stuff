using System;

namespace DocumentFactory.markdownFactory.markdownElements
{
    public class MarkdownImage : IElement
    {
        string _src;
        string _alt;
        string _title;

        public MarkdownImage(string props)
        {
            const int SRC_TXT_POS = 0;
            const int ALT_TXT_POS = 1;
            const int TITLE_TXT_POS = 2;

            if (props == null)
                throw new ArgumentNullException("Props passed are null in Image element");

            var props_list = props.Split(';');

            if (props_list.Length > 3)
                throw new ArgumentException("Too many props for Image element");

            _src = props_list[SRC_TXT_POS];
            _alt = props_list[ALT_TXT_POS];
            _title = props_list[TITLE_TXT_POS];
        }

        public override string ToString()
        {
            return $"![{_alt}]({_src} \"{_title}\")";
        }
    }
}

namespace DocumentFactory
{
    public interface IDocument
    {
        void AddElement(IElement element);
        void RunDocument();
    }
}

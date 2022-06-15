namespace DocumentBuilderLibrary.patterns
{
    public interface IComposite
    {
        void AddChild(IComposite child);
        string Print(int depth);
    }
}

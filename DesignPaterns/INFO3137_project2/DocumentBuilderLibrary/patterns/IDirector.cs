namespace DocumentBuilderLibrary.patterns
{
    public interface IDirector
    {
        void BuildBranch();
        void BuildLeaf();
        void CloseBranch();
    }
}

namespace DeclLangsNotes.Singleton
{
    public class CatSingleton
    {
        private static CatSingleton _instance;
        private string name;

        private CatSingleton(string name)
        {
            this.name = name;
        }

        public static CatSingleton getInstance()
        { 
            if (_instance == null)
            {
                _instance = new CatSingleton("Snuffles"); // Just an arbitrary default name - it doesn't matter what you chose here
                return _instance;
            }
            else
            {
                return _instance;
            }
        }

        public string getName()
        {
            return name;
        }
    }
}

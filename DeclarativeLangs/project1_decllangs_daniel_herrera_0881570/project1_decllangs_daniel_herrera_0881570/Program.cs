using project1_decllangs_daniel_herrera_0881570.frontend;

namespace project1_decllangs_daniel_herrera_0881570
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // data.json and schema.json are both in the DEBUG folder
            const string DATA_FILE_NAME = "data.json";
            const string SCHEMA_FILE_NAME = "schema.json";

            // start state machine
            new ChoiceInterface(DATA_FILE_NAME, SCHEMA_FILE_NAME);
        }
    }
}

namespace ProjektarbeitLibrary
{
    /// <summary>
    /// Interface welches wir für Klassen StorageFile/StorageREST/StorageSQL verwenden und welche in der Klasse Storage referenziert wird
    /// </summary>
    public interface IStorage
    {
        void Load(string[] connection);

        void Verify(string[] connection, string keyword, string answer);

        void Save(string[] connection, string keyword, string answer);

        public void SaveLine(string[] connection, string keyword, string answer, int line);

        public string ReadLine(string[] connection, int line);

        string FindAnswer(string keyword);

    }
}

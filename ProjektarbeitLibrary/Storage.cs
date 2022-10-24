namespace ProjektarbeitLibrary
{
    /// <summary>
    /// Klasse Storage welche für einfaches hinzufügen von weiteren Eingabetypen erlaubt
    /// </summary>
    public class Storage
    {
        private IStorage _storage;

        public Storage(IStorage storage)
        {
            _storage = storage;
        }

        private string[] _connection;
        public string[] Connection
        {
            get 
            {
                return _connection; 
            }
            set 
            {
                _connection = value; 
            }
        }

        private string _keyword;
        public string Keyword
        {
            get
            {
                return _keyword;
            }
            set
            {
                _keyword = value;
            }
        }
        private string _answer;
        public string Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
            }
        }

        private int _line;
        public int Line
        {
            get
            {
                return _line;
            }
            set
            {
                _line = value;
            }
        }

        public void Load()
        {
            try
            {
                _storage.Load(_connection);
            }
            catch (Exception ex)
            {
                // Fehler Zurückgeben
                throw new Exception(ex.Message);
            }
        }

        public string FindAnswer()
        {
            try
            {
                string answer = _storage.FindAnswer(_keyword);
                return answer;
            }
            catch (Exception ex)
            {
                // Fehler Zurückgeben
                throw new Exception(ex.Message);
            }
        }

        public void Save()
        {
            try
            {
                _storage.Verify(_connection, _keyword, _answer);
                _storage.Save(_connection, _keyword, _answer);
            }
            catch (Exception ex)
            {
                // Fehler Zurückgeben
                throw new Exception(ex.Message);
            }
        }

        public void SaveLine()
        {
            try
            {
                _storage.Verify(_connection, _keyword, _answer);
                _storage.SaveLine(_connection, _keyword, _answer, _line);
            }
            catch (Exception ex)
            {
                // Fehler Zurückgeben
                throw new Exception(ex.Message);
            }
        }
        public string ReadLine()
        {
            try
            {
                string line = _storage.ReadLine(_connection, _line);
                return line;
            }
            catch (Exception ex)
            {
                // Fehler Zurückgeben
                throw new Exception(ex.Message);
            }
        }
    }
}

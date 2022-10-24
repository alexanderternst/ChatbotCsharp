using File = System.IO.File;

namespace ProjektarbeitLibrary
{
    /// <summary>
    /// Storage Klasse welche Daten in Liste ladet und Antwort herausfindet
    /// </summary>
    public class StorageFile : IStorage
    {
        /// <summary>
        /// Listen für Nachrichten (Typ Message)
        /// </summary>
        List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Load() Methode welche Daten von Dokument in Liste lädt
        /// </summary>
        public void Load(string[] connection)
        {
            int rowNumber = 0;
            try
            {
                var reader = new StreamReader($@"{connection[0]}");

                while (!reader.EndOfStream)
                {
                    rowNumber++;
                    var line = reader.ReadLine();

                    if (rowNumber == 1)
                        continue;

                    var data = line.Split(";");

                    string keyword = data[0];
                    string answer = data[1];

                    // Keyword und Answer werden der Liste angefügt
                    Messages.Add(new Message(keyword, answer));
                }
            }
            catch (Exception ex)
            {
                // Fehler Zurückgeben
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// FindAnswer() Methode welche Antwort zu Keyword findet
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <returns>Antwort</returns>
        public string FindAnswer(string keyword)
        {
            string answer = string.Empty;

            foreach (Message mes in Messages)
            {
                // If Befehl der nach Keyword in Liste sucht
                if (keyword.Contains(mes.Keyword, StringComparison.OrdinalIgnoreCase))
                {
                    answer += mes.Answer;
                    answer += ". ";
                }
            }
            return answer;
        }

        /// <summary>
        /// Verify Methode welche Eingabe verifiziert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="answer"></param>
        /// <param name="keyword"></param>
        /// <exception cref="Exception"></exception>
        public void Verify(string[] connection, string answer, string keyword)
        {
            // Bei Bereits existierenden Eingaben Fehler ausgeben
            var check = File.ReadAllLines($@"{connection[0]}").Contains(keyword + ";" + answer);
            if (check == true)
                throw new Exception("Eingabe ist bereits vorhanden");

            // Bei Leerer Eingabe Fehler ausgeben
            if (keyword == string.Empty || answer == string.Empty || keyword.Contains(";") || answer.Contains(";"))
            {
                throw new Exception("Ungültige Eingabe");
            }
        }

        /// <summary>
        /// Methode welche Keyword und Antwort in Dokument schreibt
        /// </summary>
        /// <param name="file"></param>
        /// <param name="keyword"></param>
        /// <param name="answer"></param>
        /// <param name="linenr"></param>
        /// <param name="action"></param>
        public void Save(string[] connection, string keyword, string answer)
        {
            try
            {
                string operator1 = ";";

                using (StreamWriter writer = new StreamWriter($@"{connection[0]}", append: true))
                {
                    writer.WriteLine(keyword + operator1 + answer);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Methode welche Keyword und Antwort in Dokument an spezifische Stelle schreibt
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="keyword"></param>
        /// <param name="answer"></param>
        /// <param name="line"></param>
        public void SaveLine(string[] connection, string keyword, string answer, int line)
        {
            try
            {
                string[] arrLine = File.ReadAllLines($@"{connection[0]}");
                arrLine[line] = keyword + ";" + answer;
                File.WriteAllLines(connection[0], arrLine);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Methode welche Spezifische Linie in Dokument liest
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="line"></param>
        /// <returns>Output der Linie</returns>
        public string ReadLine(string[] connection, int line)
        {
            try
            {
                string lineOut = File.ReadLines($@"{connection[0]}").Skip(line).Take(1).First();
                return lineOut;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
using System.Data.SqlClient;
using System.Data;

namespace ProjektarbeitLibrary
{
    /// <summary>
    /// Storage Klasse welche Daten in Liste ladet und Antwort herausfindet
    /// </summary>
    public class StorageSQL : IStorage
    {
        /// <summary>
        /// Listen für Nachrichten (Typ Message)
        /// </summary>
        List<Message> Messages { get; set; } = new List<Message>();

        static SqlConnection conn;

        /// <summary>
        /// Tabellen für auslesen von SQL Tabellen
        /// </summary>
        private DataTable dataTable = new DataTable();
        private DataTable dataTable2 = new DataTable();


        /// <summary>
        /// Load() Methode welche Daten von Datenbank in Liste lädt
        /// </summary>
        public void Load(string[] connection)
        {
            try
            {
                string connString = $@"{connection[0]}";
                string query = $"SELECT * FROM {connection[1]}";

                conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                conn.Close();
                da.Dispose();

                foreach (DataRow row in dataTable.Rows)
                {
                    string keyword = row["Keyword"].ToString();
                    string answer = row["Answer"].ToString();
                    Messages.Add(new Message(keyword, answer));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// FindAnswer() Methode welche Antwort zu Keyword findet
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <returns>Gibt Antwort zurück</returns>
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
            if (keyword == string.Empty || answer == string.Empty)
            {
                throw new Exception("Ungültige Eingabe");
            }
        }

        /// <summary>
        /// Methode welche Keyword und Antwort in Datenbank schreibt
        /// </summary>
        /// <param name="file"></param>
        /// <param name="keyword"></param>
        /// <param name="answer"></param>
        /// <param name="linenr"></param>
        /// <param name="action"></param>
        /// <exception cref="Exception"></exception>
        public void Save(string[] connection, string keyword, string answer)
        {
            try
            {
                string connString = $@"{connection[0]}";
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand cmdInsert = conn.CreateCommand();
                cmdInsert.CommandText = $"INSERT INTO {connection[1]}(Keyword, Answer) VALUES('{keyword}', '{answer}')";
                cmdInsert.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Methode welche Keyword und Antwort in Datenbank an spezifische Stelle schreibt
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="keyword"></param>
        /// <param name="answer"></param>
        /// <param name="line"></param>
        public void SaveLine(string[] connection, string keyword, string answer, int line)
        {
            try
            {
                string connString = $@"{connection[0]}";
                conn = new SqlConnection(connString);
                conn.Open();

                SqlCommand cmdInsert = conn.CreateCommand();
                cmdInsert.CommandText = $"UPDATE {connection[1]} SET Keyword = '{keyword}', Answer = '{answer}' WHERE ID = {line}; ";
                cmdInsert.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Methode welche Spezifische Linie in Datenbank liest
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public string ReadLine(string[] connection, int line)
        {
            try
            {
                string connString = $@"{connection[0]}";
                string query = $"SELECT * FROM {connection[1]} WHERE ID = {line};";

                conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable2);
                conn.Close();
                da.Dispose();

                string keyword = string.Empty;
                string answer = string.Empty;

                foreach (DataRow row in dataTable2.Rows)
                {
                    keyword = row["Keyword"].ToString();
                    answer = row["Answer"].ToString();
                }

                return $"{keyword};{answer}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
using System.Net;

namespace ProjektarbeitLibrary
{

    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class StorageREST : IStorage
    {

        public httpVerb HttpMethod { get; set; }

        List<Message> Messages { get; set; } = new List<Message>();

        public StorageREST()
        {
            HttpMethod = httpVerb.GET;

        }
        /// <summary>
        /// Load() Methode welche Daten von Dokument in Liste lädt
        /// </summary>
        /// <returns></returns>
        public void Load(string[] connection)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(connection[0]);

            request.Method = HttpMethod.ToString();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException($"error code: {response.StatusCode}");
                }
                //Prozess gibt ein response stream json

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {

                    var line = reader.ReadLine();

                    //string writefile = "storageML.json";

                    //if (File.Exists(writefile))
                    //{
                    //    writefile.Replace(line, "");
                    //}

                    //using (StreamWriter writer = new StreamWriter(writefile, append: true))
                    //{
                    //    writer.WriteLine(line);
                    //}

                    //var config = new ConfigurationBuilder()
                    //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    //    .AddJsonFile("storageML.json").Build();
                    //var section = config.GetSection(nameof(JSON));
                    //var configChatbot = section.Get<JSON>();





                    var data = line.Split(':');

                    for (int i = 0; i < data.Length - 2; i += 2)
                    {
                        string keyword = data[1 + i]
                            .Replace(",\"answer\"", "")
                            .Replace("\"", "");
                        keyword.Trim();

                        string answer = data[2 + i]
                            .Replace("},{\"keyword\"", "")
                            .Replace("\"", "")
                            .Replace("}]", "");
                        answer.Trim();

                        Console.WriteLine($"Keyword = {keyword}");
                        Console.WriteLine($"Answer = {answer}");

                        Messages.Add(new Message(keyword, answer));
                    }
                }

            }
        }


        /// <summary>
        /// findAnswer() Methode welche Antwort zu Keyword findet
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <returns>Gibt Antwort zurück</returns>
        public string FindAnswer(string keyword)
        {
            string answer = string.Empty;

            // Foreach Schleife welche durch jedes Objekt in der Liste geht
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
        /// Gibt die Linie zurück wo man angeben hat mit den Values
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public string ReadLine(string[] connection, int line)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(connection[0]);

            string keyword = string.Empty;
            string answer = string.Empty;
            HttpMethod = httpVerb.GET;

            request.Method = HttpMethod.ToString();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException($"error code: {response.StatusCode}");
                }
                //Prozess gibt ein response stream json

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {

                    var lessen = reader.ReadLine();

                    var data = lessen.Split(':');

                    keyword = data[1 + line]
                        .Replace(",\"answer\"", "")
                        .Replace("\"", "");
                    keyword.Trim();

                    answer = data[2 + line]
                        .Replace("},{\"keyword\"", "")
                        .Replace("\"", "")
                        .Replace("}]", "");
                    answer.Trim();

                    Console.WriteLine($"Keyword = {keyword}");
                    Console.WriteLine($"Answer = {answer}");
                }
            }
            return $"{keyword};{answer}";
        }

        /// <summary>
        /// Noch nicht Implemtiert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="keyword"></param>
        /// <param name="answer"></param>
        /// <exception cref="ApplicationException"></exception>
        public void Save(string[] connection, string keyword, string answer)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(connection[0]);

            HttpMethod = httpVerb.POST;

            request.Method = HttpMethod.ToString();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException($"error code: {response.StatusCode}");
                }
                //Prozess gibt ein response stream json

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {

                    //var lessen = reader.ReadLine();

                    //var data = lessen.Split(':');

                    //keyword = data[1 + line]
                    //    .Replace(",\"answer\"", "")
                    //    .Replace("\"", "");
                    //keyword.Trim();

                    //answer = data[2 + line]
                    //    .Replace("},{\"keyword\"", "")
                    //    .Replace("\"", "")
                    //    .Replace("}]", "");
                    //answer.Trim();

                    //Console.WriteLine($"Keyword = {keyword}");
                    //Console.WriteLine($"Answer = {answer}");
                }
            }
        }

        /// <summary>
        /// Noch nicht Implemtiert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="keyword"></param>
        /// <param name="answer"></param>
        /// <param name="line"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SaveLine(string[] connection, string keyword, string answer, int line)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Noch nicht Implemtiert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="keyword"></param>
        /// <param name="answer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Verify(string[] connection, string keyword, string answer)
        {
            throw new NotImplementedException();
        }
    }
}
using ProjektarbeitWPFar;

namespace ProjektarbeitLibrary
{
    public static class BotEngine
    {
        //Storage storage { get; set; } = new Storage();
        //Message messages { get; set; } = new Message();

        /// <summary>
        /// Methode GetAnswer welche von GUI aufgerufen wird, Eingabe erwartet, Methoden aufruft, und Antwort zurückgibt
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>Antwort auf Eingabe</returns>
        public static string GetAnswer(string keyword)
        {
            try
            {
                Config c = Config.GetInstance();
                if (c.selection == "SQL")
                {
                    // Dependency Injection
                    Storage s1 = new Storage(new StorageSQL());

                    string[] conn = { c.connectionString, c.table };
                    s1.Connection = conn;
                    s1.Load();

                    s1.Keyword = keyword;
                    string answer = s1.FindAnswer();

                    if (answer == string.Empty)
                    {
                        throw new InputExceptionCB("Ungültige Eingabe", 0);
                    }
                    else
                    {
                        return answer;
                    }
                }
                else if (c.selection == "CSV")
                {
                    Storage s1 = new Storage(new StorageFile());

                    string[] conn = { c.file };
                    s1.Connection = conn;
                    s1.Load();

                    s1.Keyword = keyword;
                    string answer = s1.FindAnswer();

                    if (answer == string.Empty)
                    {
                        throw new InputExceptionCB("Ungültige Eingabe", 0);
                    }
                    else
                    {
                        return answer;
                    }
                }
                else if (c.selection == "REST")
                {
                    Storage s1 = new Storage(new StorageREST());

                    string[] conn = { "http://localhost:5000/api/registrations/", "Get" };
                    s1.Connection = conn;
                    s1.Load();

                    s1.Keyword = keyword;
                    string answer = s1.FindAnswer();

                    if (answer == string.Empty)
                    {
                        throw new InputExceptionCB("Ungültige Eingabe", 0);
                    }
                    else
                    {
                        return answer;
                    }
                }
                else
                {
                    throw new Exception("Konfiguration fehlt");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
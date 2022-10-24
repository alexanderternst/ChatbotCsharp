namespace ProjektarbeitLibrary
{
    /// <summary>
    /// Klasse Message welche Properties und Konstruktoren für Liste besitzt
    /// </summary>
    public class Message
    {
        // Properties
        public string Keyword { get; set; }

        public string Answer { get; set; }

        // Konstruktoren
        public Message()
        {

        }

        public Message(string keyword, string answer)
        {
            Keyword = keyword;
            Answer = answer;
        }

    }
}
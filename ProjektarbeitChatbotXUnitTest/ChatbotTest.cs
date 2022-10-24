using ProjektarbeitLibrary;
using System;
using System.Data.SqlTypes;

namespace ProjektarbeitChatBotXuntitTest
{
    public class ChatBotTest
    {
        /// <summary>
        /// Testet alle Klasse und prüft ob es richtige antwort gibt.
        /// </summary>
        [Fact]
        public void GetAnserENTestCSV()
        {
            //Arange 
            var input = "4+4";

            var result = BotEngine.GetAnswer(input);

            Assert.Equal("8th. ", result);
        }

        /// <summary>
        /// Testet alle Klasse und prüft ob es richtige antwort gibt.
        /// </summary>
        [Fact]
        public void GetAnserDETestCSV()
        {
            //Arange 
            var input = "4+4";

            var result = BotEngine.GetAnswer(input);

            Assert.Equal("8. ", result);
        }

        /// <summary>
        /// Prüft ob das ob die Methode funktioniert, es gibt Lehren string zurück. Weil es in
        /// findAnswer wir die liste nicht refenzieren
        /// </summary>
        [Fact]
        public void FindAnswerTestCSV()
        {
            var sut = new Storage(new StorageFile());
            var input = "Hi";

            sut.Keyword = input;

            var result = sut.FindAnswer();

            Assert.Equal("", result);
        }

        /// <summary>
        /// Prüft ob das ob die Methode funktioniert, es gibt Lehren string zurück. Weil es in
        /// findAnswer wir die liste nicht refenzieren
        /// </summary>
        [Fact]
        public void FindAnswerTestSQL()
        {
            var sut = new Storage(new StorageSQL());
            var input = "Hi";

            sut.Keyword = input;

            var result = sut.FindAnswer();

            Assert.Equal("", result);
        }

        /// <summary>
        /// Testet alle Klasse und prüft ob es richtige antwort gibt.
        /// </summary>
        [Fact]
        public void GetAnserENTestSQL()
        {
            //Arange 
            var input = "4+4";

            var result = BotEngine.GetAnswer(input);

            Assert.Equal("8th. ", result);
        }

        /// <summary>
        /// Testet alle Klasse und prüft ob es richtige antwort gibt.
        /// </summary>
        [Fact]
        public void GetAnserDETestSQL()
        {
            //Arange 
            var input = "4+4";

            var result = BotEngine.GetAnswer(input);

            Assert.Equal("8. ", result);
        }

        /// <summary>
        /// Prüft ob das ob die Methode funktioniert, es gibt Lehren string zurück. Weil es in
        /// findAnswer wir die liste nicht refenzieren
        /// </summary>
        [Fact]
        public void FindAnswerTestREST()
        {
            var sut = new Storage(new StorageSQL());
            var input = "Hi";

            sut.Keyword = input;

            var result = sut.FindAnswer();

            Assert.Equal("", result);
        }

        /// <summary>
        /// Testet alle Klasse und prüft ob es richtige antwort gibt.
        /// </summary>
        [Fact]
        public void GetAnserENTestREST()
        {
            //Arange 
            var input = "4+4";

            var result = BotEngine.GetAnswer(input);

            Assert.Equal("8. 8th. ", result);
        }

        /// <summary>
        /// Testet alle Klasse und prüft ob es richtige antwort gibt.
        /// </summary>
        [Fact]
        public void GetAnserDETestREST()
        {
            //Arange 
            var input = "4+4";

            var result = BotEngine.GetAnswer(input);

            Assert.Equal("8. 8th. ", result);
        }

    }
}
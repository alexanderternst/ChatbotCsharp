using System;
using System.IO;
using System.Windows;
using System.Speech.Synthesis;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using ProjektarbeitLibrary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Configuration;
using System.Security.Policy;

namespace ProjektarbeitWPFar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                string fileName = "appsettings.json";
                string jsonString = File.ReadAllText(fileName);
                var config = JsonSerializer.Deserialize<RootObject>(jsonString);

                Config c = Config.GetInstance();

                c.selection = config.ChatbotConfig.selection;
                c.language = config.ChatbotConfig.language;
                c.file_DE = config.ChatbotConfig.file_DE;
                c.file_EN = config.ChatbotConfig.file_EN;
                c.file = config.ChatbotConfig.file;

                c.connectionString = config.ChatbotConfig.connectionString;
                c.table_DE = config.ChatbotConfig.table_DE;
                c.table_EN = config.ChatbotConfig.table_EN;
                c.table = config.ChatbotConfig.table;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Fehler {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Methode welche auf Button Senden reagiert, Methoden aufruft und Antwort ausgibt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSenden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Config c = Config.GetInstance();

                string question = tbEingabeFenster.Text;

                string answer = BotEngine.GetAnswer(question);

                var synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();
                var builder = new PromptBuilder();

                // Ausgabe der Antwort
                tbAusgabeFenster.Text += $"{DateTime.Now} \nUser: {question}\n";
                tbAusgabeFenster.Text += $"Bot: {answer}\n\n";
                tbEingabeFenster.Clear();


                if (c.language == "EN")
                {
                    builder.StartVoice(new CultureInfo("en-US"));
                    builder.AppendText(answer);
                    builder.EndVoice();
                }
                else if (c.language == "DE")
                {
                    builder.StartVoice(new CultureInfo("de-DE"));
                    builder.AppendText(answer);
                    builder.EndVoice();
                }

                synthesizer.Speak(builder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Fehler {ex.Message}",
                "Begrüssung",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Methode welche auf Knopfdruck reagiert und Programm schliesst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($" Wollen Sie wirklich beenden?",
                "Begrüssung",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Close();
        }

        /// <summary>
        /// Methode welche auf schliessen des Programmes reagiert und Einträge in Log-Datei macht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string text = tbAusgabeFenster.Text;
                string texttr = text.Trim();
                if (string.IsNullOrEmpty(texttr))
                {
                    return;
                }
                else
                {
                    string writefile = "log.txt";
                    using (StreamWriter writer = new StreamWriter(writefile, append: true))
                    {
                        writer.WriteLine(texttr + "\n");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Fehler {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Methode welche neues Fenster öffnet, welches Eingabe von neuem Keyword/neuer Antwort erlaubt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Erfassung win1 = new Erfassung();
            win1.Show();
        }

        /// <summary>
        /// Methode welche neues Fenster öffnet, welches Lesen und Modifizieren von Keywords und Antworten erlaubt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            ErfassungNr win2 = new ErfassungNr();
            win2.Show();
        }

        /// <summary>
        /// Methode welche neues Fenster öffnet, welches Modifizieren von Config File erlaubt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow win3 = new ConfigWindow();
            win3.Show();
        }

        /// <summary>
        /// Methode welche About Fenster öffnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chatbot, made by Alexander Ernst and Raphael Hug \nCopyright @ 2022", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
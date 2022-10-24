using Microsoft.Extensions.Configuration;
using ProjektarbeitLibrary;
using System;
using System.IO;
using System.Windows;

namespace ProjektarbeitWPFar
{
    /// <summary>
    /// Interaction logic for Erfassung.xaml
    /// </summary>
    public partial class Erfassung : Window
    {
        public Erfassung()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Methode welche auf Knopdruck neue Erfassung in Dokument oder Datenbank erstellt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Config c = Config.GetInstance();

                string s = cbSprache.Text;
                string keyword = tbKeyword.Text;
                string answer = tbAntwort.Text;

                if (c.selection == "SQL")
                {
                    Storage s1 = new Storage(new StorageSQL());
                    s1.Keyword = keyword;
                    s1.Answer = answer;

                    if (s == "Deutsche Erfassung")
                    {
                        string[] conn = { c.connectionString, c.table_DE };
                        s1.Connection = conn;
                        s1.Save();
                    }
                    else if (s == "Englische Erfassung")
                    {
                        string[] conn = { c.connectionString, c.table_EN };
                        s1.Connection = conn;
                        s1.Save();
                    }
                    else
                    {
                        throw new Exception("Bitte Sprachauswahl angeben");
                    }
                }
                else if (c.selection == "CSV")
                {
                    Storage s1 = new Storage(new StorageFile());
                    s1.Keyword = keyword;
                    s1.Answer = answer;
                    if (s == "Deutsche Erfassung")
                    {
                        string[] conn = { c.file_DE };
                        s1.Connection = conn;
                        s1.Save();
                    }
                    else if (s == "Englische Erfassung")
                    {
                        string[] conn = { c.file_EN };
                        s1.Connection = conn;
                        s1.Save();
                    }
                    else
                    {
                        throw new Exception("Bitte Sprachauswahl angeben");
                    }
                }

                MessageBox.Show($" Neues Schlüsselwort und neue Antwort erfasst"
                , "Information"
                , MessageBoxButton.OK,
                MessageBoxImage.Information);

                // Loggen des Erfassten Eintrages
                string writefile = "log.txt";
                using (StreamWriter writer = new StreamWriter(writefile, append: true))
                {
                    writer.WriteLine($"Datum: {DateTime.Now}\nNeues Schlüsselwort und neue Antwort erfasst.\nSchlüsselwort: {keyword}, Antwort: {answer}, Sprache: {s}\n");
                }

                tbKeyword.Clear();
                tbAntwort.Clear();
                Close();

            }
            // Exception
            catch (FileNotFoundException fex)
            {
                MessageBox.Show($" File Fehler: {fex.Message}",
                    "Fehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Fehler: {ex.Message}",
                "Fehler",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Methode welche auf Knopfdruck Fenster schliesst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
using Microsoft.Extensions.Configuration;
using ProjektarbeitLibrary;
using System;
using System.IO;
using System.Windows;

namespace ProjektarbeitWPFar
{
    /// <summary>
    /// Interaction logic for ErfassungNr.xaml
    /// </summary>
    public partial class ErfassungNr : Window
    {
        public ErfassungNr()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Methode welche auf Knopfdruck Reagiert und Bestehende Erfassung in SQL oder Datei modifiziert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Config c = Config.GetInstance();

                string keyword = tbKeyword.Text;
                string answer = tbAntwort.Text;
                string s = cbSprache.Text;
                int num = Convert.ToInt32(tbNummer.Text);

                if (c.selection == "SQL")
                {
                    Storage s1 = new Storage(new StorageSQL());

                    s1.Keyword = keyword;
                    s1.Answer = answer;
                    s1.Line = num;

                    if (s == "Sprachauswahl")
                    {
                        throw new Exception("Ungültige Eingabe");
                    }
                    else if (s == "Deutsch")
                    {
                        string[] conn = { c.connectionString, c.table_DE };
                        s1.Connection = conn;
                        s1.SaveLine();
                    }
                    else if (s == "Englisch")
                    {
                        string[] conn = { c.connectionString, c.table_EN };
                        s1.Connection = conn;
                        s1.SaveLine();
                    }
                }
                else if (c.selection == "CSV") 
                {
                    Storage s1 = new Storage(new StorageFile());

                    s1.Keyword = keyword;
                    s1.Answer = answer;
                    s1.Line = num;

                    if (s == "Sprachauswahl")
                    {
                        throw new Exception("Ungültige Eingabe");
                    }
                    else if (s == "Deutsch")
                    {
                        string[] conn = { c.file_DE };
                        s1.Connection = conn;
                        s1.SaveLine();
                    }
                    else if (s == "Englisch")
                    {
                        string[] conn = { c.file_EN };
                        s1.Connection = conn;
                        s1.SaveLine();
                    }
                }
                // Loggen des neu erstellten Eintrages
                string writefile = "log.txt";
                using (StreamWriter writer = new StreamWriter(writefile, append: true))
                {
                    writer.WriteLine($"Datum: {DateTime.Now}\nSchlüsselwort und Antwort auf Linie {num} modifiziert.\nSchlüsselwort: {keyword}, Antwort: {answer}, Sprache: {s}\n");
                }

                MessageBox.Show($" Reihe {num} modifiziert.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                Close();
            }
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
        /// Methode welche auf Knopdruck reagiert und entweder in Deutschem oder Englischem Dokument/Datenbank spezifische Linie liest und bearbeitung ermöglicht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Config c = Config.GetInstance();

                if (c.selection == "SQL")
                {
                    //Storage s1 = new Storage(new StorageFile());
                    Storage s1 = new Storage(new StorageSQL());

                    string s = cbSprache.Text;
                    int num = Convert.ToInt32(tbNummer.Text);
                    if (num <= 0)
                        throw new Exception("Ungültige Nummer");

                    s1.Line = num;
                    string line = string.Empty;

                    // Auswahlselektion zwischen verschiedenen Dokumenten und Lesen der spezifischen Linie
                    if (s == "Deutsch")
                    {

                        string[] conn = { c.connectionString, c.table_DE };
                        s1.Connection = conn;
                        line = s1.ReadLine();
                    }
                    else if (s == "Englisch")
                    {
                        string[] conn = { c.connectionString, c.table_EN };
                        s1.Connection = conn;
                        line = s1.ReadLine();
                    }
                    else
                    {
                        throw new Exception("Bitte Sprachauswahl angeben");
                    }

                    // Ausgabe
                    var data = line.Split(";");
                    string keyword = data[0];
                    string answer = data[1];

                    tbKeyword.Text = keyword;
                    tbAntwort.Text = answer;
                }
                else if (c.selection == "CSV")
                {
                    Storage s1 = new Storage(new StorageFile());

                    string s = cbSprache.Text;
                    int num = Convert.ToInt32(tbNummer.Text);
                    if (num <= 0)
                        throw new Exception("Ungültige Nummer");

                    s1.Line = num;
                    string line = string.Empty;

                    if (s == "Deutsch")
                    {
                        string[] conn = { c.file_DE };
                        s1.Connection = conn;
                        line = s1.ReadLine();
                    }
                    else if (s == "Englisch")
                    {
                        string[] conn = { c.file_EN };
                        s1.Connection = conn;
                        line = s1.ReadLine();
                    }
                    else
                    {
                        throw new Exception("Bitte Sprachauswahl angeben");
                    }

                    // Ausgabe
                    var data = line.Split(";");
                    string keyword = data[0];
                    string answer = data[1];

                    tbKeyword.Text = keyword;
                    tbAntwort.Text = answer;
                }

                // Abfrage ob Eintrag auch gleich Modifiziert werden soll
                MessageBoxResult result = MessageBox.Show(" Wollen Sie diesen Eintrag modifizieren",
                    "Modifizieren",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    btnRead.Visibility = Visibility.Hidden;
                    btnSend.Visibility = Visibility.Visible;
                    cbSprache.Visibility = Visibility.Hidden;
                    btnSend.IsDefault = true;

                    tbKeyword.IsReadOnly = false;
                    tbAntwort.IsReadOnly = false;
                    tbNummer.IsReadOnly = true;
                    tblNum.Text = "Modifizierende Reihe";
                }
            }
            catch (FileNotFoundException fnex)
            {
                MessageBox.Show($" File Fehler: {fnex.Message}",
                        "Fehler",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
            catch (FormatException fex)
            {
                MessageBox.Show($" Format Fehler: {fex.Message}",
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
        /// Methode welche auf Knopdruck Fenster schliesst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

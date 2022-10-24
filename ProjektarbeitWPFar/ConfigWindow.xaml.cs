using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ProjektarbeitWPFar
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();

            try
            {
                Config c = Config.GetInstance();

                tbConnectionString.Text = c.connectionString;
                tbTableDE.Text = c.table_DE;
                tbTableEN.Text = c.table_EN;

                tbFileDE.Text = c.file_DE;
                tbFileEN.Text = c.file_EN;

                tblSprache.Text += c.language;
                tblFormat.Text += c.selection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Fehler: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Methode welche auf Knopdruck Fenster für Dateiselektion öffnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileDE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.DefaultExt = ".csv"; // Default file extension
                dialog.Filter = "CSV (Comma delimited)|*.csv"; // Filter files by extension

                // Show open file dialog box
                bool? result = dialog.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dialog.FileName;
                    tbFileDE.Text = filename;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Fehler: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Methode welche auf Knopdruck Fenster für Dateiselektion öffnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileEN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.DefaultExt = ".csv"; // Default file extension
                dialog.Filter = "CSV (Comma delimited)|*.csv"; // Filter files by extension

                // Show open file dialog box
                bool? result = dialog.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dialog.FileName;
                    tbFileEN.Text = filename;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Fehler: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Methode welche auf Knopfdurck neue Einstellungen erfasst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSprache.Text == "Sprachauswahl" || cbFormat.Text == "Format")
                {
                    MessageBox.Show("Fehlende Angaben", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string sprache = "";
                    string file = "";
                    string table = "";
                    if (cbSprache.Text == "Deutsch")
                    {
                        sprache = "DE";
                        file = tbFileDE.Text;
                        table = tbTableDE.Text;
                    }
                    else if (cbSprache.Text == "Englisch")
                    {
                        sprache = "EN";
                        file = tbFileEN.Text;
                        table = tbTableEN.Text;
                    }

                    var chatbotConfig = new ChatbotConfig
                    {
                        language = sprache,
                        selection = cbFormat.Text,
                        file_DE = tbFileDE.Text,
                        file_EN = tbFileEN.Text,
                        file = file,
                        connectionString = tbConnectionString.Text,
                        table_DE = tbTableDE.Text,
                        table_EN = tbTableEN.Text,
                        table = table
                    };

                    string fileName = "appsettings.json";

                    string jsonString = JsonSerializer.Serialize(chatbotConfig);
                    string jsonStringFinal = "{\"ChatbotConfig\": \n" + jsonString + "\n}";
                    File.WriteAllText(fileName, jsonStringFinal);

                    MessageBox.Show(" Einstellungen erfolgreich geändert", "Einstellungen", MessageBoxButton.OK, MessageBoxImage.Information);

                    string jsonStringRead = File.ReadAllText(fileName);
                    var config = JsonSerializer.Deserialize<RootObject>(jsonStringRead);

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

                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Fehler: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Methode welche Fenster schliesst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

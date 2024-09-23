using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notendurchschnitt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TsNoten notes = new TsNoten();
        public MainWindow()
        {
            InitializeComponent();
            bnLoeschen.IsEnabled = false;
            updateUI();
        }

        private void updateUI()
        {
            int index = lbxNoten.SelectedIndex;
            lbxNoten.Items.Clear();
            for (int i = 0; i < notes.Anzahl; i++)
            {
                lbxNoten.Items.Add(notes.Lesen(i));
            }
            lbxNoten.SelectedIndex = index < notes.Anzahl ? index : -1;

            lblAnzahlNoten.Content = $"{notes.Anzahl} Noten eingetragen";

            try
            {
                tbSchnitt.Text = $"{notes.Durchschnitt:F1}";
            }
            catch 
            {
                tbSchnitt.Text = "";
            }
        }

        private void hinzufuegen(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = (!double.TryParse(tbGewicht.Text, out double gewicht) ? MessageBox.Show("Eingabe konnte nicht verarbeitet werden, möchten sie fortfahren?", "Fehler", MessageBoxButton.OKCancel, MessageBoxImage.Question) : MessageBoxResult.OK);
            if (mbr == MessageBoxResult.OK)
            {
                mbr = (!double.TryParse(tbNeueNote.Text, out double neueNote) ? MessageBox.Show("Eingabe konnte nicht verarbeitet werden, möchten sie fortfahren?", "Fehler", MessageBoxButton.OKCancel, MessageBoxImage.Question) : MessageBoxResult.OK);
                if (mbr == MessageBoxResult.OK)
                {
                    try
                    {
                        notes.Hinzufuegen(neueNote, gewicht);
                        updateUI();
                        tbNeueNote.Focus();
                        tbNeueNote.SelectAll();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Fehler");
                    }
                }
            }

        }

        private void loeschen(object sender, RoutedEventArgs e)
        {
            if (lbxNoten.SelectedIndex == -1)
            {
                MessageBox.Show("Keine Note ausgewählt!", "Fehler");
            }
            else
            {
                notes.Loeschen(lbxNoten.SelectedIndex);
                updateUI();
            }
        }

        private void lbxNoten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxNoten.SelectedIndex != -1)
            {
                bnLoeschen.IsEnabled = true;
            }
            else 
            { 
                bnLoeschen.IsEnabled = false;
            }
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new()
            {
                Filter = "JSON documents |*.json|XML documents |*.xml",
                FileName = "testfile",
                DefaultExt = ".json",
                InitialDirectory = System.IO.Path.GetFullPath(@"C:\Users\Silas\Desktop")
            };
            if (sfd.ShowDialog() == true)
               switch (System.IO.Path.GetExtension(sfd.FileName).Trim().ToLower())
                {
                    case "xml":
                        //TO-DO: Implement
                        break;
                    case "json":
                        notes.ExportJSON(sfd.FileName);
                        break;
                    default:
                        break;
                }
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Filter = "JSON documents |*.json",
                FileName = "testfile",
                DefaultExt = ".json",
                InitialDirectory = System.IO.Path.GetFullPath(@"C:\Users\Silas\Desktop")
            };
            if (ofd.ShowDialog() == true)
                notes.ImportJSON(ofd.FileName);
        }
    }
}
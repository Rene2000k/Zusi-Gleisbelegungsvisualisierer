using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gleisbelegungsvisualisierer
{
    /// <summary>
    /// Interaktionslogik für MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            MainWindow = (MainWindow)DataContext;
        }

        private void ButtonSelectTimetablePath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TextBoxTimetablePath.Text = dialog.SelectedPath;
            }
        }

        private void ButtonAddNewOperatingSite_Click(object sender, RoutedEventArgs e)
        {
            string newOperatingSiteName = TextBoxNewOperatingSiteName.Text;
            if (newOperatingSiteName != "")
            {
                MainWindow.AddOperatingSite(newOperatingSiteName);
                ListViewOpperatingSites.SelectedItem = MainWindow.OperatingSites.Last();
            }
            TextBoxNewOperatingSiteName.Text = "";
        }

        private void ButtonAddNewTrack_Click(object sender, RoutedEventArgs e)
        {
            string newTrackName = TextBoxNewTrackName.Text;
            if (newTrackName != "" && ListViewOpperatingSites.SelectedItem != null)
            {
                OperatingSite selectedOperatingSite = (OperatingSite)ListViewOpperatingSites.SelectedItem;
                selectedOperatingSite.AddTrack(newTrackName);
                ListViewTracks.SelectedItem = selectedOperatingSite.Tracks.Last();
            }
            TextBoxNewTrackName.Text = "";
        }

        private void ButtonAddNewSignal_Click(object sender, RoutedEventArgs e)
        {
            string newSignalName = TextBoxNewSignalName.Text;
            if (newSignalName != "" && ListViewTracks.SelectedItem != null)
            {
                Track selectedTrack = (Track)ListViewTracks.SelectedItem;
                selectedTrack.AddSignal(newSignalName);
                ListViewSignals.SelectedItem = selectedTrack.Signals.Last();
            }
            TextBoxNewSignalName.Text = "";
        }

        private void ButtonDeleteOperatingSite_Click(object sender, RoutedEventArgs e)
        {
            OperatingSite selectedOperatingSite = (OperatingSite)ListViewOpperatingSites.SelectedItem;
            MainWindow.RemoveOperatingSite(selectedOperatingSite);
        }

        private void ButtonDeleteTrack_Click(object sender, RoutedEventArgs e)
        {
            OperatingSite selectedOperatingSite = (OperatingSite)ListViewOpperatingSites.SelectedItem;
            Track selectedTrack = (Track)ListViewTracks.SelectedItem;
            selectedOperatingSite.RemoveTrack(selectedTrack);
        }

        private void ButtonDeleteSignal_Click(object sender, RoutedEventArgs e)
        {
            Track selectedTrack = (Track)ListViewTracks.SelectedItem;
            string selectedSignal = (string)ListViewSignals.SelectedItem;
            selectedTrack.RemoveSignal(selectedSignal);
        }

        private void ListViewOpperatingSites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OperatingSite selectedOperatingSite = (OperatingSite)ListViewOpperatingSites.SelectedItem;
            if (selectedOperatingSite != null)
            {
                ListViewTracks.ItemsSource = selectedOperatingSite.Tracks;
            }
            else
            {
                ListViewTracks.ItemsSource = null;
            }
        }

        private void ListViewTracks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Track selectedTrack = (Track)ListViewTracks.SelectedItem;
            if (selectedTrack != null)
            {
                ListViewSignals.ItemsSource = selectedTrack.Signals;
            }
            else
            {
                ListViewSignals.ItemsSource = null;
            }
        }

        private MainWindow MainWindow { get; }

    }
}

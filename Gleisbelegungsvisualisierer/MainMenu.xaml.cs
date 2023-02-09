using Gleisbelegungsvisualisierer.Model;
using System.Collections.Generic;
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
        public MainMenu(ObservableCollection<OperatingSite> operatingSites, ObservableCollection<string> timetablePaths)
        {
            InitializeComponent();
            MainWindow = (MainWindow)DataContext;

            PathsToTimetableFolder = timetablePaths;

            ListViewOpperatingSites.ItemsSource = operatingSites;
            TextBoxTimetablePath.ItemsSource = timetablePaths;
            TextBoxTimetablePath.Text = timetablePaths[0];

        }

        private void ButtonSelectTimetablePath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TextBoxTimetablePath.Text = dialog.SelectedPath;

                if (!PathsToTimetableFolder.Contains(dialog.SelectedPath))
                {
                    PathsToTimetableFolder.Insert(0, dialog.SelectedPath);

                    while (PathsToTimetableFolder.Count > MAX_LEN_TIMETABLE_LIST)
                    {
                        PathsToTimetableFolder.RemoveAt(PathsToTimetableFolder.Count - 1);
                    }
                }
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
            Signal selectedSignal = (Signal)ListViewSignals.SelectedItem;
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
        private ObservableCollection<string> PathsToTimetableFolder { get; set; }
        private const int MAX_LEN_TIMETABLE_LIST = 5;

    }
}

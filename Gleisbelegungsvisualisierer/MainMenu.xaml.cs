using Gleisbelegungsvisualisierer.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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

            PathsToTimetableFolder = timetablePaths;
            OperatingSites = operatingSites;

            ListViewOpperatingSites.ItemsSource = OperatingSites;
            TextBoxTimetablePath.ItemsSource = PathsToTimetableFolder;
            TextBoxTimetablePath.SelectedIndex = 0;
            TextBoxTimetablePath.SelectionChanged += TextBoxTimetablePath_SelectionChanged;
        }

        private void TextBoxTimetablePath_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string selectedPath = e.AddedItems[0] as string;
                if (selectedPath != null && PathsToTimetableFolder.Contains(selectedPath) && PathsToTimetableFolder[0] != selectedPath)
                {
                    PathsToTimetableFolder.Remove(selectedPath);
                    PathsToTimetableFolder.Insert(0, selectedPath);
                    TextBoxTimetablePath.SelectedIndex = 0;
                }
            }
        }

        private void ButtonSelectTimetablePath_Click(object sender, RoutedEventArgs e)
        {
            string path = Utils.ShowFolderDialog();
            if (path != null && !PathsToTimetableFolder.Contains(path))
            {
                PathsToTimetableFolder.Insert(0, path);
                TextBoxTimetablePath.SelectedIndex = 0;
                while (PathsToTimetableFolder.Count > MAX_LEN_TIMETABLE_LIST)
                {
                    PathsToTimetableFolder.RemoveAt(PathsToTimetableFolder.Count - 1);
                }
            }
        }

        private void ButtonAddNewOperatingSite_Click(object sender, RoutedEventArgs e)
        {
            string newOperatingSiteName = TextBoxNewOperatingSiteName.Text;
            if (newOperatingSiteName != "")
            {
                OperatingSite newOS = AddOperatingSite(newOperatingSiteName);
                ListViewOpperatingSites.SelectedItem = newOS;
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
            if (selectedOperatingSite != null)
            {
                RemoveOperatingSite(selectedOperatingSite);
            }
        }

        private void ButtonDeleteTrack_Click(object sender, RoutedEventArgs e)
        {
            OperatingSite selectedOperatingSite = (OperatingSite)ListViewOpperatingSites.SelectedItem;
            Track selectedTrack = (Track)ListViewTracks.SelectedItem;
            if (selectedOperatingSite != null && selectedTrack != null)
            {
                selectedOperatingSite.RemoveTrack(selectedTrack);
            }
        }

        private void ButtonDeleteSignal_Click(object sender, RoutedEventArgs e)
        {
            Track selectedTrack = (Track)ListViewTracks.SelectedItem;
            Signal selectedSignal = (Signal)ListViewSignals.SelectedItem;
            if (selectedTrack != null && selectedSignal != null)
            {
                selectedTrack.RemoveSignal(selectedSignal);
            }
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

        private void ButtonTrackUp_Click(object sender, RoutedEventArgs e)
        {
            SwapTracks(-1);
        }

        private void ButtonTrackDown_Click(object sender, RoutedEventArgs e)
        {
            SwapTracks(1);
        }

        public OperatingSite AddOperatingSite(string name)
        {
            int index = 0;
            while (index < OperatingSites.Count && name.CompareTo(OperatingSites[index].Name) > 0)
            {
                index++;
            }

            OperatingSite os = new OperatingSite(name);
            OperatingSites.Insert(index, os);
            return os;
        }

        internal void RemoveOperatingSite(OperatingSite operatingSite)
        {
            OperatingSites.Remove(operatingSite);
        }

        /*
         * swap the selected track with another track in the tracklist
         * upOrDown: integer for the position to change (e.g. -1 --> one place up; +2 --> two places down)
         */
        private void SwapTracks(int upOrDown)
        {
            OperatingSite selectedOperatingSite = (OperatingSite)ListViewOpperatingSites.SelectedItem;
            Track selectedTrack = (Track)ListViewTracks.SelectedItem;
            if (selectedOperatingSite != null && selectedTrack != null)
            {
                ObservableCollection<Track> trackList = selectedOperatingSite.Tracks;
                for (int i = 0; i < trackList.Count; i++)
                {
                    if (trackList[i] == selectedTrack && i + upOrDown >= 0 && i + upOrDown < trackList.Count)
                    {
                        trackList[i] = trackList[i + upOrDown];
                        trackList[i + upOrDown] = selectedTrack;
                        break;
                    }
                }
            }
        }

        public ObservableCollection<OperatingSite> OperatingSites { get; }
        public ObservableCollection<string> PathsToTimetableFolder { get; set; }

        private const int MAX_LEN_TIMETABLE_LIST = 5;

        
    }
}

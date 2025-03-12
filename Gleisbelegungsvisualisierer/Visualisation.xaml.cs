using Gleisbelegungsvisualisierer.Model;
using Gleisbelegungsvisualisierer.VisualisationElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Gleisbelegungsvisualisierer
{
    /// <summary>
    /// Interaktionslogik für Visualisation.xaml
    /// </summary>
    public partial class Visualisation : UserControl
    {
        private OperatingSite lastAnalyzedOperatingSite;
        private bool showAlternativeOccupations = true;

        public Visualisation(ObservableCollection<OperatingSite> operatingSites)
        {
            InitializeComponent();
            ZusiController = new ZusiController();
            ComboBoxOperatingSites.ItemsSource = operatingSites;
        }

        private void ButtonAnalyse_Click(object sender, RoutedEventArgs e)
        {
            ContentPanel.Children.Clear();
            OperatingSite selectedOperatingSite = (OperatingSite)ComboBoxOperatingSites.SelectedItem;
            if (selectedOperatingSite != null)
            {
                selectedOperatingSite.ResetTrackOccupations();
                StartAnalysing(selectedOperatingSite, true);
            }
        }

        private void StartAnalysing(OperatingSite operatingSite, bool isFullAnalysis)
        {
            showAlternativeOccupations = CheckBoxShowAlternatives.IsChecked ?? true;
            
            if (isFullAnalysis)
            {
                ZusiController.GetTrackOccupationsForOperatingSite(((MainMenu)DataContext).TextBoxTimetablePath.Text, operatingSite);
                lastAnalyzedOperatingSite = operatingSite;
                GenerateVisualisation(operatingSite);
            }
            else
            {
                // Only update existing track occupations' visibility
                foreach (Track track in operatingSite.Tracks)
                {
                    foreach (TrackOccupation occupation in track.TrackOccupations)
                    {
                        if (occupation.ElementOnCanvas != null && occupation.IsAlternativeOccupation)
                        {
                            occupation.ElementOnCanvas.Visibility = 
                                showAlternativeOccupations 
                                ? Visibility.Visible 
                                : Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckBoxShowAlternatives_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (lastAnalyzedOperatingSite != null)
            {
                StartAnalysing(lastAnalyzedOperatingSite, false);
            }
        }

        public void GenerateVisualisation(OperatingSite operatingSite)
        {
            ContentPanel.Children.Clear();
            TimeSpan startTime = operatingSite.FindStartTime();
            TimeSpan endTime = operatingSite.FindEndTime();
            TimelineCanvas timeline = new TimelineCanvas(startTime, endTime);
            Column timelineColumn = new Column(timeline.Width, "", timeline);
            DockPanel dp = new DockPanel();
            DockPanel.SetDock(timelineColumn, Dock.Left);
            dp.Children.Add(timelineColumn);
            ContentPanel.Children.Add(dp);
            foreach (Track track in operatingSite.Tracks)
            {
                Column trackOccupationColumn = CreateColumn(track, startTime, endTime);
                ContentPanel.Children.Add(trackOccupationColumn);
            }
        }

        private Column CreateColumn(Track track, TimeSpan startTime, TimeSpan endTime)
        {
            List<TrackOccupation> TrackOccupations = track.GetTrackOccupationsAsOrderedList();
            TrackCanvas canvas = new TrackCanvas();
            canvas.PopulateCanvas(TrackOccupations, startTime, endTime, showAlternativeOccupations);
            return new Column(TrackCanvas.WIDTH + Column.BORDER_THICKNESS, track.Name, canvas);
        }

        private ZusiController ZusiController { get; }
    }
}

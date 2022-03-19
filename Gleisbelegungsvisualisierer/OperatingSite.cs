using Gleisbelegungsvisualisierer.VisualisationElements;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Gleisbelegungsvisualisierer
{
    public class OperatingSite
    {
        //For XML-Serailization
        private OperatingSite() { }
        public OperatingSite(string name)
        {
            this.Name = name;
            Tracks = new ObservableCollection<Track>();
        }

        public Track AddTrack(string name)
        {
            Track track = new Track(name);
            Tracks.Add(track);
            return track;
        }

        public void RemoveTrack(Track track)
        {
            Tracks.Remove(track);
        }

        public TimeSpan FindStartTime()
        {
            TimeSpan startingTime = new TimeSpan(24, 0, 0);
            foreach (Track track in Tracks)
            {
                foreach (TrackOccupation trackOccupation in track.TrackOccupations)
                {
                    if (startingTime == null || (startingTime > trackOccupation.Arrival.TimeOfDay))
                    {
                        startingTime = trackOccupation.Arrival.TimeOfDay;
                    }
                }
            }
            return startingTime;
        }

        public TimeSpan FindEndTime()
        {
            TimeSpan endingTime = new TimeSpan(0, 0, 0);
            foreach (Track track in Tracks)
            {
                foreach (TrackOccupation trackOccupation in track.TrackOccupations)
                {
                    if (endingTime == null || (endingTime < trackOccupation.Departure.TimeOfDay))
                    {
                        endingTime = trackOccupation.Departure.TimeOfDay;
                    }
                }
            }
            return endingTime;
        }

        public void GenerateVisualisation(Visualisation visualisation)
        {
            TimeSpan startTime = FindStartTime();
            TimeSpan endTime = FindEndTime();
            TimelineCanvas timeline = new TimelineCanvas(startTime, endTime);
            Column timelineColumn = new Column(timeline.Width, "", timeline);
            DockPanel dp = new DockPanel();
            DockPanel.SetDock(timelineColumn, Dock.Left);
            dp.Children.Add(timelineColumn);
            visualisation.ContentPanel.Children.Add(dp);
            foreach (Track track in Tracks)
            {
                Column trackOccupationColumn = track.CreateColumn(startTime, endTime);
                visualisation.ContentPanel.Children.Add(trackOccupationColumn);
            }
        }

        public void ResetTrackOccupations()
        {
            foreach (Track track in Tracks)
            {
                track.RemoveAllTrackOccupations();
            }
        }

        public string Name { get; set; }
        public ObservableCollection<Track> Tracks { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}

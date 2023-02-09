using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.Model
{
    public class OperatingSite
    {
        //For XML-Serailization
        private OperatingSite() { }
        public OperatingSite(string name)
        {
            Name = name;
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
                    if (startingTime == null || startingTime > trackOccupation.Arrival.TimeOfDay)
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
                    if (endingTime == null || endingTime < trackOccupation.Departure.TimeOfDay)
                    {
                        endingTime = trackOccupation.Departure.TimeOfDay;
                    }
                }
            }
            return endingTime;
        }

        public void ResetTrackOccupations()
        {
            foreach (Track track in Tracks)
            {
                track.RemoveAllTrackOccupations();
            }
        }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement(ElementName = "Track")]
        public ObservableCollection<Track> Tracks { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}

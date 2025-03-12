using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                foreach (
                    TimeSpan arrival in track.TrackOccupations.Select(trackOccupation => trackOccupation.Arrival.TimeOfDay)
                )
                {
                    if (startingTime == null || startingTime > arrival)
                    {
                        startingTime = arrival;
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
                foreach (
                    TimeSpan departure in track.TrackOccupations.Select(trackOccupation => trackOccupation.Departure.TimeOfDay)
                )
                {
                    if (endingTime == null || endingTime < departure)
                    {
                        endingTime = departure;
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

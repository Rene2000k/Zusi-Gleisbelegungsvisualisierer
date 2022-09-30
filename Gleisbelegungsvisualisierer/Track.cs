using Gleisbelegungsvisualisierer.VisualisationElements;
using Gleisbelegungsvisualisierer.XML_Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer
{
    public class Track
    {
        //For XML-Serailization
        private Track()
        {
            // Generating an empty track occupation list while deserializing
            TrackOccupations = new List<TrackOccupation>();
        }

        public Track(string name)
        {
            Name = name;
            Signals = new ObservableCollection<string>();
            TrackOccupations = new List<TrackOccupation>();
        }

        public void AddSignal(string name)
        {
            Signals.Add(name);
        }

        public void RemoveSignal(string name)
        {
            Signals.Remove(name);
        }

        public TrackOccupation AddTrackOccupation(DateTime arrival, DateTime departure, XMLTrain train)
        {
            TrackOccupation trackOccupation = new TrackOccupation(arrival, departure, train);
            TrackOccupations.Add(trackOccupation);
            return trackOccupation;
        }

        public void RemoveTrackOccupation(TrackOccupation trackOccupation)
        {
            TrackOccupations.Remove(trackOccupation);
        }

        public void RemoveAllTrackOccupations()
        {
            TrackOccupations.Clear();
        }

        internal List<TrackOccupation> GetTrackOccupationsAsOrderedList()
        {
            return TrackOccupations.OrderBy(trackOccupation => trackOccupation.Arrival).ToList();
        }

        public string Name { get; set; }
        public ObservableCollection<string> Signals { get; set; }
        [XmlIgnore]
        public List<TrackOccupation> TrackOccupations { get; set; }
        [XmlIgnore]
        public Column TrackColumn { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

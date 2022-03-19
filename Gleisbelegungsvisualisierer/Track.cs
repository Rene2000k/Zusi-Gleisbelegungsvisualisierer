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
            TrackOccupations = new List<TrackOccupation>();
        }

        public Track(string name)
        {
            this.Name = name;
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

        public Column CreateColumn(TimeSpan startTime, TimeSpan endTime)
        {
            TrackOccupations = TrackOccupations.OrderBy(trackOccupation => trackOccupation.Arrival).ToList();
            TrackCanvas canvas = new TrackCanvas();
            canvas.PopulateCanvas(TrackOccupations, startTime, endTime);
            TrackColumn = new Column(TrackCanvas.WIDTH + Column.BORDER_THICKNESS, Name, canvas);
            return TrackColumn;
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

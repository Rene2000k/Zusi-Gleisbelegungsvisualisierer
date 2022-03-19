using Gleisbelegungsvisualisierer.XML_Structure;
using System;
using System.Windows.Controls;

namespace Gleisbelegungsvisualisierer
{
    public class TrackOccupation
    {
        public TrackOccupation(DateTime arrival, DateTime departure, XMLTrain train)
        {
            Arrival = arrival;
            Departure = departure;
            Train = train;
            HorizontalPositionOnCanvas = -1;
            ElementOnCanvas = null;
        }

        public DateTime Arrival { get; }
        public DateTime Departure { get; }
        public XMLTrain Train { get; set; }
        public int HorizontalPositionOnCanvas { get; set; }
        public Border ElementOnCanvas { get; set; }

        public override string ToString()
        {
            return string.Format("Gleisbelegung: [Ank: {0}, Abf: {1}, Zug: {2}]", Arrival, Departure, Train.ToString());
        }
    }
}

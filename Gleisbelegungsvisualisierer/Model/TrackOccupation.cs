using Gleisbelegungsvisualisierer.XML_Structure;
using System;
using System.Windows.Controls;

namespace Gleisbelegungsvisualisierer.Model
{
    public class TrackOccupation
    {
        public TrackOccupation(DateTime? arrival, DateTime? departure, XMLTrain train)
        {
            Train = train;
            HorizontalPositionOnCanvas = -1;
            ElementOnCanvas = null;
            NoTime = false;
            Transiting = false;

            if (arrival == null && departure == null)
            {
                NoTime = true;
                Arrival = new DateTime(0);
                Departure = new DateTime(0);
            }
            else if (arrival == null)
            {
                Transiting = true;
                Departure = departure.Value;
                Arrival = departure.Value.Add(new TimeSpan(0, -1, 0));
            }
            else if (departure == null)
            {
                Transiting = true;
                Arrival = arrival.Value;
                Departure = arrival.Value.Add(new TimeSpan(0, 1, 0));
            }
            else
            {
                Arrival = arrival.Value;
                Departure = departure.Value;
            }

        }

        public DateTime Arrival { get; }
        public DateTime Departure { get; }
        public XMLTrain Train { get; set; }
        public int HorizontalPositionOnCanvas { get; set; }
        public Border ElementOnCanvas { get; set; }
        public bool Transiting { get; set; }
        public bool NoTime { get; set; }

        public override string ToString()
        {
            return string.Format("Gleisbelegung: [Ank: {0}, Abf: {1}, Zug: {2}]", Arrival, Departure, Train.ToString());
        }
    }
}

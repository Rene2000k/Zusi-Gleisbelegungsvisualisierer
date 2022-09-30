using Gleisbelegungsvisualisierer.XML_Structure;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer
{
    public class ZusiController
    {
        private const string TRAIN_FILE_ENING = "*.trn";

        private XmlSerializer zusiDeserializer;
        public ZusiController()
        {
            zusiDeserializer = new XmlSerializer(typeof(Zusi));
        }


        public void GetTrackOccupationsForOperatingSite(string folderPath, OperatingSite operatingSite)
        {
            string[] allFileNames = GetAllFilesInDirectory(folderPath);
            foreach (string fileName in allFileNames)
            {
                Zusi deserializedTrain = DeserializeFile(fileName);
                AnalyseTrain(deserializedTrain, operatingSite);
            }
        }

        private string[] GetAllFilesInDirectory(string folderPath)
        {
            try
            {
                return Directory.GetFiles(folderPath, TRAIN_FILE_ENING);
            }
            catch
            {
                return new string[0];
            }
        }

        private Zusi DeserializeFile(string fileName)
        {
            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                return (Zusi)zusiDeserializer.Deserialize(reader);
            }
        }

        private void AnalyseTrain(Zusi deserializedTrain, OperatingSite operatingSite)
        {
            foreach (TimetableEntry timetableEntry in deserializedTrain.Train.TimetableEntries)
            {
                if (timetableEntry.OperatingSite == operatingSite.Name)
                {
                    foreach (TimetableSignalEntry signal in timetableEntry.TimetableSignalEntries)
                    {
                        foreach (Track track in operatingSite.Tracks)
                        {
                            GenerateTrackOccupation(track, signal, timetableEntry, deserializedTrain);
                        }
                    }
                }
            }
        }

        private void GenerateTrackOccupation(Track track, TimetableSignalEntry signal, TimetableEntry timetableEntry, Zusi deserializedTrain)
        {
            if (track.Signals.Contains(signal.TimetableSignal))
            {
                XMLTrain xmlTrain = deserializedTrain.Train;
                DateTime? arrival = null;
                if (timetableEntry.ArrivalTime != null)
                {
                    arrival = DateTime.Parse(timetableEntry.ArrivalTime);
                }
                DateTime? departure = null;
                if (timetableEntry.DepartureTime != null)
                {
                    departure = DateTime.Parse(timetableEntry.DepartureTime);
                }
                if (arrival != null || departure != null)
                {
                    if (arrival == null)
                    {
                        arrival = departure.Value.Add(new TimeSpan(0, -1, 0));
                    }
                    if (departure == null)
                    {
                        departure = arrival.Value.Add(new TimeSpan(0, 1, 0));
                    }
                    track.AddTrackOccupation(arrival.Value, departure.Value, xmlTrain);
                }
            }
        }

    }
}

using Gleisbelegungsvisualisierer.Model;
using Gleisbelegungsvisualisierer.XML_Structure;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gleisbelegungsvisualisierer.VisualisationElements
{
    /*
     * This is representing a track column in the visualisation
     * It is filled with all trains that occupy the track associated with this column
     */
    public class TrackCanvas : Canvas
    {
        public const int PIXEL_FOR_MINUTE = 10;
        public const int WIDTH = 300;

        public TrackCanvas()
        {
            Width = WIDTH;
            Background = new SolidColorBrush(Colors.AliceBlue);
        }

        public void PopulateCanvas(List<TrackOccupation> trackOccupations, TimeSpan startTime, TimeSpan endTime)
        {
            StartTime = startTime;
            EndTime = endTime;

            TimeSpan timeSpan = endTime.Subtract(startTime);
            if (timeSpan.TotalSeconds < 0)
            {
                timeSpan = new TimeSpan(0, 0, 0);
            }

            Height = timeSpan.TotalMinutes * PIXEL_FOR_MINUTE;
            AddTrackOccupations(trackOccupations);
        }

        private void AddTrackOccupations(List<TrackOccupation> trackOccupations)
        {
            foreach (TrackOccupation trackOccupation in trackOccupations)
            {
                if (!trackOccupation.NoTime)
                {
                    DateTime departure = trackOccupation.Departure;
                    DateTime arrival = trackOccupation.Arrival;
                    TimeSpan occupationTime = departure.Subtract(arrival);
                    TimeSpan startDiff = arrival.TimeOfDay.Subtract(StartTime);

                    Block block = new Block(trackOccupation, arrival, departure, occupationTime.TotalMinutes * PIXEL_FOR_MINUTE);
                    FindHorizontalPosition(block, trackOccupation, trackOccupations);

                    trackOccupation.ElementOnCanvas = block.Border;
                    Children.Add(block.Border);
                    SetTop(block.Border, startDiff.TotalMinutes * PIXEL_FOR_MINUTE);
                }
            }
        }


        private void FindHorizontalPosition(Block block, TrackOccupation currentTO, List<TrackOccupation> trackOccupations)
        {
            //Prerequisite: trackOccupations is sorted by arrival time
            List<int> occupiedPositions = new List<int>();
            //1. step: find all overlapping track occupations
            List<TrackOccupation> overlappingTOs = new List<TrackOccupation>();
            TimeSpan currentTOArrival = currentTO.Arrival.TimeOfDay;
            TimeSpan currentTODeparture = currentTO.Departure.TimeOfDay;
            foreach (TrackOccupation trackOccupation in trackOccupations)
            {
                TimeSpan arrival = trackOccupation.Arrival.TimeOfDay;
                TimeSpan departure = trackOccupation.Departure.TimeOfDay;
                if (((currentTOArrival >= arrival && currentTOArrival <= departure) || (currentTODeparture >= arrival && currentTODeparture <= departure))
                    && trackOccupation.HorizontalPositionOnCanvas != -1)
                {
                    overlappingTOs.Add(trackOccupation);
                }
            }
            int countOverlappings = overlappingTOs.Count;
            double width = (double) WIDTH / (countOverlappings + 1);
            //2. step: mark positions as occupied and recalculate position
            foreach (TrackOccupation trackOccupation in overlappingTOs)
            {
                int pos = trackOccupation.HorizontalPositionOnCanvas;
                occupiedPositions.Add(pos);
                Border elemtentOnCanvas = trackOccupation.ElementOnCanvas;
                elemtentOnCanvas.Width = width;
                elemtentOnCanvas.Margin = new System.Windows.Thickness { Top = 0, Left = width * pos, Right = 0, Bottom = 0 };
            }
            //3. step: sort track occupation in right position depending on arrival time
            int position = 0;
            while (occupiedPositions.Contains(position)) position++;
            //4. step: recalculate position on canvas for new to
            currentTO.HorizontalPositionOnCanvas = position;
            block.Border.Width = width;
            block.Border.Margin = new System.Windows.Thickness { Top = 0, Left = width * position, Right = 0, Bottom = 0 };
        }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}

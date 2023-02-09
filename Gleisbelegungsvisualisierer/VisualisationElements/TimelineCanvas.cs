using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gleisbelegungsvisualisierer.VisualisationElements
{
    /*
     *  This Element is representing the timeline on the left side
     */
    class TimelineCanvas : Canvas
    {
        private const int WIDTH = 50;

        public TimelineCanvas(TimeSpan startTime, TimeSpan endTime)
        {
            Width = WIDTH;
            StartTime = startTime;
            EndTime = endTime;

            TimeSpan timeSpan = endTime.Subtract(startTime);
            if (timeSpan.TotalSeconds < 0)
            {
                timeSpan = new TimeSpan(0, 0, 0);
            }

            Height = timeSpan.TotalMinutes * TrackCanvas.PIXEL_FOR_MINUTE;
            CreateCanvas();
        }

        private void CreateCanvas()
        {
            CreateHours();
        }

        private void CreateHours()
        {
            //calculate reamining minutes to next full hour from start time
            int minutesToFirstHour = (int)(60 - StartTime.TotalMinutes) % 60;
            TimeSpan timeSpan = StartTime.Add(new TimeSpan(0, minutesToFirstHour, 0));
            int numHour = 0;
            while (timeSpan <= EndTime)
            {
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new System.Windows.Thickness { Top = 2, Bottom = 0, Left = 0, Right = 0 };
                TextBlock text = new TextBlock();
                text.Text = timeSpan.ToString(@"hh\:mm");
                text.FontSize = 20;
                border.Child = text;
                Children.Add(border);
                SetTop(border, numHour * 60 * TrackCanvas.PIXEL_FOR_MINUTE + minutesToFirstHour * TrackCanvas.PIXEL_FOR_MINUTE);
                numHour++;
                timeSpan = timeSpan.Add(new TimeSpan(1, 0, 0));
            }
        }

        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }
    }
}

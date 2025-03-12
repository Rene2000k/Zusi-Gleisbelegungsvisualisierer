using Gleisbelegungsvisualisierer.Model;
using Gleisbelegungsvisualisierer.XML_Structure;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gleisbelegungsvisualisierer.VisualisationElements
{
    public class Block
    {
        public Block(TrackOccupation trackOccupation, DateTime arrival, DateTime departure, double height)
        {
            Border = new Border();
            Text = new TextBlock();
            Color = GetColorForTrainType(trackOccupation.Train);

            XMLTrain train = trackOccupation.Train;
            string BorderText = train.TrainType + " " + train.TrainNumber + "\n" + train.TrainRun + "\n" +
                "Ankunft: " + arrival.TimeOfDay.ToString(@"hh\:mm") + "\n" +
                "Abfahrt: " + departure.TimeOfDay.ToString(@"hh\:mm");
            Text.Text = BorderText;

            Border.Child = Text;
            Border.ToolTip = new ToolTip().Content = BorderText;
            Border.Height = height;

            ColorBorder(trackOccupation);
        }

        private static Color GetColorForTrainType(XMLTrain train)
        {
            switch (train.TrainType)
            {
                case "ICE":
                    return Colors.Red;
                case "IC":
                    return Colors.Orange;
                case "IR":
                    return Colors.LightBlue;
                case "RE":
                    return Colors.LightGreen;
                case "RB":
                    return Colors.Yellow;
                default:
                    return Colors.LightGray;
            }
        }

        private void ColorBorder(TrackOccupation trackOccupation)
        {
            if (trackOccupation.IsAlternativeOccupation)
            {
                Border.Background = Brushes.Transparent;
                Border.BorderBrush = new SolidColorBrush(Color);
                Border.BorderThickness = new Thickness(2);
            }
            else if (trackOccupation.Transiting)
            {
                var vb = new VisualBrush();
                vb.Viewport = new Rect(0, 0, 10, 10);  // Increased from 10 to 20
                vb.ViewportUnits = BrushMappingMode.Absolute;
                vb.Viewbox = new Rect(0, 0, 10, 10);   // Increased from 10 to 20
                vb.ViewboxUnits = BrushMappingMode.Absolute;
                vb.TileMode = TileMode.Tile;

                var drawing = new GeometryGroup();
                drawing.Children.Add(new LineGeometry(new Point(0, 10), new Point(10, 0)));
                drawing.Children.Add(new LineGeometry(new Point(-5, 5), new Point(5, -5)));
                drawing.Children.Add(new LineGeometry(new Point(5, 15), new Point(15, 5)));

                var path = new Path
                {
                    Data = drawing,
                    Stroke = new SolidColorBrush(Color),
                    StrokeThickness = 2  // Increased from 1 to 2
                };

                vb.Visual = path;
                Border.Background = vb;
                Border.BorderBrush = new SolidColorBrush(Color);
                Border.BorderThickness = new Thickness(2);
            }
            else
            {
                Border.Background = new SolidColorBrush(Color);
            }
        }

        public Border Border { get; set; }
        public TextBlock Text { get; set; }
        public Color Color { get; set; }
    }
}

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
            if (trackOccupation.Transiting)
            {
                // TODO: Schraffierungen
                Border.Background = new SolidColorBrush(Color);

                //VisualBrush vb = new VisualBrush();
                //vb.Viewport = new Rect(0, 0, 5, 5);
                //vb.ViewboxUnits = BrushMappingMode.Absolute;
                //vb.Viewbox = new Rect(0, 0, 5, 5);
                //vb.ViewboxUnits = BrushMappingMode.Absolute;
                //vb.TileMode = TileMode.Tile;
                //vb.Visual = new Path
                //{
                //    Data = Geometry.Parse("M 0 5 L 5 0 M -2 2 L 2 -2 M 3 7 L 7 3"),
                //    Stroke = new SolidColorBrush(Color)
                //};
                //Border.Background = vb;
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

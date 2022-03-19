using System.Windows.Controls;
using System.Windows.Media;

namespace Gleisbelegungsvisualisierer.VisualisationElements
{
    /*
     *  This element is prepresenting a column with a canvas in it.
     *  Additionally it creates a field for text above the canvas, primarily used for the track names, and a border around everything
     *  It is used for the timeline and all track columns
     */
    public class Column : Border
    {
        public const int BORDER_THICKNESS = 2;

        public Column(double width, string trackName, Canvas canvas)
        {
            Width = width;
            BorderBrush = new SolidColorBrush(Colors.Black);
            BorderThickness = new System.Windows.Thickness { Top = BORDER_THICKNESS, Bottom = BORDER_THICKNESS, Left = BORDER_THICKNESS, Right = 0 };

            Grid column = new Grid();
            RowDefinition firstRow = new RowDefinition();
            firstRow.Height = new System.Windows.GridLength(50);
            column.RowDefinitions.Add(firstRow);
            column.RowDefinitions.Add(new RowDefinition());

            Label trackNameLabel = new Label();
            trackNameLabel.Content = trackName;
            trackNameLabel.FontSize = 24;
            Grid.SetRow(trackNameLabel, 0);
            column.Children.Add(trackNameLabel);

            Grid.SetRow(canvas, 1);
            column.Children.Add(canvas);

            Child = column;
        }
    }
}

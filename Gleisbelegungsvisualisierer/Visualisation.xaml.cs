using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Gleisbelegungsvisualisierer
{
    /// <summary>
    /// Interaktionslogik für Visualisation.xaml
    /// </summary>
    public partial class Visualisation : UserControl
    {
        public Visualisation(MainWindow mw, ObservableCollection<OperatingSite> operatingSites)
        {
            InitializeComponent();
            ComboBoxOperatingSites.ItemsSource = operatingSites;
            MainWindow = mw;
        }


        private void ButtonAnalyse_Click(object sender, RoutedEventArgs e)
        {
            ContentPanel.Children.Clear();
            OperatingSite selectedOperatingSite = (OperatingSite)ComboBoxOperatingSites.SelectedItem;
            selectedOperatingSite.ResetTrackOccupations();
            MainWindow.StartAnalysing(selectedOperatingSite);
        }

        MainWindow MainWindow;
    }
}

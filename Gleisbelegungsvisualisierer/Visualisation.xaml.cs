using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            MainWindow.StartAnalysing(selectedOperatingSite);
        }

        XMLController Controller { get; }
        MainWindow MainWindow;
    }
}

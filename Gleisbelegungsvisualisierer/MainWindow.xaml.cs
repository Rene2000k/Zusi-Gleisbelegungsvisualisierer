using Gleisbelegungsvisualisierer.XML_Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            XMLController = new XMLController();

            UserSettings settings = XMLController.DeserializeSettingsFromFile();
            OperatingSites = new ObservableCollection<OperatingSite>(settings.OperatingSites);

            MainMenu = new MainMenu(this);
            Visualisation = new Visualisation(this, OperatingSites);

            MainmenuTab.Content = MainMenu;
            MainMenu.ListViewOpperatingSites.ItemsSource = OperatingSites;
            VisualisationTab.Content = Visualisation;

            MainMenu.TextBoxTimetablePath.Text = settings.PathToTimetableFolder;
        }

        public void AddOperatingSite(string name)
        {
            OperatingSites.Add(new OperatingSite(name));
        }

        internal void RemoveOperatingSite(OperatingSite operatingSite)
        {
            OperatingSites.Remove(operatingSite);
        }

        public void StartAnalysing(OperatingSite operatingSite)
        {
            XMLController.GetTrackOccupationsForOperatingSite(MainMenu.TextBoxTimetablePath.Text, operatingSite);
            operatingSite.GenerateVisualisation(Visualisation);
        }


        private MainMenu MainMenu { get; }
        private Visualisation Visualisation { get; }
        internal ObservableCollection<OperatingSite> OperatingSites { get; }
        private XMLController XMLController { get; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSettings settings = new UserSettings(OperatingSites.ToList(), MainMenu.TextBoxTimetablePath.Text);
            XMLController.SerializeSettingsToFile(settings);
        }
    }
}

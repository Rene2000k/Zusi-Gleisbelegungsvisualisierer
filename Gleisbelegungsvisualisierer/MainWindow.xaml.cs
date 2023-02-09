using Gleisbelegungsvisualisierer.Model;
using Gleisbelegungsvisualisierer.XML_Structure;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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
            SettingController = new SettingController();

            UserSettings settings = SettingController.DeserializeSettingsFromFile();
            OperatingSites = new ObservableCollection<OperatingSite>(settings.OperatingSites);
            PathsToTimetableFolder = new ObservableCollection<string>(settings.PathsToTimetableFolder);

            MainMenu = new MainMenu(OperatingSites, PathsToTimetableFolder);
            MainMenu.DataContext = this;
            Visualisation = new Visualisation(OperatingSites);
            Visualisation.DataContext = MainMenu;

            MainmenuTab.Content = MainMenu;
            VisualisationTab.Content = Visualisation;
        }

        public void AddOperatingSite(string name)
        {
            OperatingSites.Add(new OperatingSite(name));
        }

        internal void RemoveOperatingSite(OperatingSite operatingSite)
        {
            OperatingSites.Remove(operatingSite);
        }

        private MainMenu MainMenu { get; }
        private Visualisation Visualisation { get; }
        internal ObservableCollection<OperatingSite> OperatingSites { get; }
        internal ObservableCollection<string> PathsToTimetableFolder { get; }
        private SettingController SettingController { get; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSettings settings = new UserSettings(OperatingSites.ToList(), PathsToTimetableFolder.ToList());
            SettingController.SerializeSettingsToFile(settings);
        }
    }
}

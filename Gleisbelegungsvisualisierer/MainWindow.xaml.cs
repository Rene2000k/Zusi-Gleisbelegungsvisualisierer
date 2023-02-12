using Gleisbelegungsvisualisierer.Model;
using Gleisbelegungsvisualisierer.XML_Structure;
using System.Collections.Generic;
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

            List<OperatingSite> osList = settings.OperatingSites.OrderBy(os => os.Name).ToList();
            ObservableCollection<OperatingSite>  operatingSites = new ObservableCollection<OperatingSite>(osList);
            ObservableCollection<string>  pathsToTimetableFolder = new ObservableCollection<string>(settings.PathsToTimetableFolder);

            MainMenu = new MainMenu(operatingSites, pathsToTimetableFolder);
            Visualisation = new Visualisation(operatingSites);
            Visualisation.DataContext = MainMenu;

            MainmenuTab.Content = MainMenu;
            VisualisationTab.Content = Visualisation;
        }

        private MainMenu MainMenu { get; }
        private Visualisation Visualisation { get; }
        private SettingController SettingController { get; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSettings settings = new UserSettings(MainMenu.OperatingSites.ToList(), MainMenu.PathsToTimetableFolder.ToList());
            SettingController.SerializeSettingsToFile(settings);
        }
    }
}

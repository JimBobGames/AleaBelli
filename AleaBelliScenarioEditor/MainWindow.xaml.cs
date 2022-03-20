using AleaBelli.Core.Data;
using AleaBelli.Core.Persistence;
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

namespace AleaBelliScenarioEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string scenarioFilename = "";

        private Scenario scenario;

        public MainWindow()
        {
            InitializeComponent();
           // TreeItem root = new TreeItem() { Title = "Menu" };
           // unitsTreeView.Items.Add(root);
            // BuildTree(null);
        }

        private void BuildTree(Scenario s)
        {
            this.unitsTreeView.Items.Clear();
            if (s != null)
            {
                TreeViewItem root = new TreeViewItem() { Header = s.Name, IsExpanded = true, DataContext = s};

                // add each side
                foreach (ScenarioSide ss in s.SideList)
                {
                    if (ss != null)
                    {
                        TreeViewItem sideItem = new TreeViewItem() { Header = ss.Name, IsExpanded = true, DataContext = ss };
                        root.Items.Add(sideItem);
                        foreach (Army a in ss.ArmyList)
                        {
                            TreeViewItem armyItem = new TreeViewItem() { Header = a.Name, IsExpanded = true, DataContext = a };
                            sideItem.Items.Add(armyItem);
                            foreach (Corps c in a.Corps)
                            {
                                TreeViewItem corpsItem = new TreeViewItem() { Header = c.Name, IsExpanded = true, DataContext = c };
                                armyItem.Items.Add(corpsItem);
                                foreach (ArmyDivision ad in c.Divisons)
                                {
                                    TreeViewItem divisionItem = new TreeViewItem() { Header = ad.Name, IsExpanded = true, DataContext = ad };
                                    corpsItem.Items.Add(divisionItem);
                                    foreach (Brigade b in ad.Brigades)
                                    {
                                        TreeViewItem brigadeItem = new TreeViewItem() { Header = b.Name, IsExpanded = true, DataContext = b };
                                        divisionItem.Items.Add(brigadeItem);
                                    }
                                }
                            }
                        }
                    }
                }

                unitsTreeView.Items.Add(root);
            }
        }

        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location.;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(path);

            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = ""; // Default file name
            dialog.DefaultExt = ".scn"; // Default file extension
            dialog.Filter = "Scenarios (.scn)|*.scn"; // Filter files by extension
            dialog.InitialDirectory = @"C:\Users\jamie\source\repos\Aleabelli\trunk\AleaBelli.Core\Config\Scenarios";
 
            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                scenarioFilename = dialog.FileName;

                scenario = new PersistenceManager().LoadScenario(scenarioFilename);
                if (scenario != null)
                {
                    this.Title = "Scenario Editor : " + scenario.Name;
                    BuildTree(scenario);
                }
                else
                {
                    this.Title = "Scenario Editor";
                }
            }
        }
        private void NewCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}

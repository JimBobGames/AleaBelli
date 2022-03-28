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
using TiledCS;

namespace AleaBelliScenarioEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string scenarioFilename = "";

        private Scenario scenario;

        private TiledMap map;

        public MainWindow()
        {
            InitializeComponent();
            // TreeItem root = new TreeItem() { Title = "Menu" };
            // unitsTreeView.Items.Add(root);
            // BuildTree(null);
            UpdatePanels(null);
        }

        private void BuildTree(Scenario s)
        {
            this.unitsTreeView.Items.Clear();
            if (s != null)
            {
                TreeViewItem root = new TreeViewItem() { Header = s.Name, IsExpanded = true, DataContext = s };

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

                                        foreach (Regiment r in b.Regiments)
                                        {
                                            TreeViewItem regimentItem = new TreeViewItem() { Header = r.Name, IsExpanded = true, DataContext = r };
                                            brigadeItem.Items.Add(regimentItem);
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                unitsTreeView.Items.Add(root);
                root.IsSelected = true;
                UpdatePanels(s);

            }
            else
            {
                UpdatePanels(null);
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
                    BuildMap(scenario);
                }
                else
                {
                    this.Title = "Scenario Editor";
                }
            }
        }

        /// <summary>
        /// Render the map onscreen
        /// </summary>
        /// <param name="scenario"></param>
        private void BuildMap(Scenario scenario)
        {
            this.MapCanvas.Children.Clear();    
            map = new PersistenceManager().LoadTiledMap(scenario.TilemapName);
            Console.WriteLine(map.TileWidth + "  " + map.TileHeight);



            TiledLayer[] layers = map.Layers;
            if (layers != null)
            {
                foreach (TiledLayer layer in layers)
                {
                    if(layer != null)
                    {
                        int layerid = layer.id;
                        TiledMapTileset tiledmapset = map.GetTiledMapTileset(layerid);
                        if (tiledmapset != null)
                        {
                            string tilesetsource = tiledmapset.source;
                            TiledTileset tileset= new PersistenceManager().LoadTileset(tilesetsource);
                            TiledImage image = tileset.Image;

                            var uri = new PersistenceManager().GetImageURI(image.source);
                            BitmapImage bitmap = new BitmapImage(uri);


                            TiledTile[] tileArray = tileset.Tiles;

                            int rows = map.Width;
                            int cols = map.Height;

                            for (int i = 0; i < layer.data.Length; i++)
                            {


                                int tileId = layer.data[i];
                                if (tileId > 0)
                                {
                                   TiledTile tile =   map.GetTiledTile(tiledmapset, tileset, tileId);
                                    TiledSourceRect tsr = map.GetSourceRect(tiledmapset, tileset, tileId);

                                    int row = i / rows;
                                    int col = i % cols;

                                    byte flags = layer.dataRotationFlags[i];
                                    Image img = new Image();
                                    img.Source = bitmap;
                                    img.Width = 640;
                                    img.Height = 640;
                                    img.ClipToBounds = true;
                                    RectangleGeometry rect = new RectangleGeometry();
                                    rect.Rect = new Rect() { Width = 32, Height = 32, X = tsr.x,  Y = tsr.y };
                                    img.Clip = rect;

                                    Canvas.SetLeft(img, col * 32 - tsr.x);
                                    Canvas.SetTop(img, row * 32 - tsr.y);
                                    this.MapCanvas.Children.Add(img);
                                }

                            }
                        }
                    }
                }
            }
        }

        private void NewCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void unitsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SimpleObject actualItem = null;
            if (unitsTreeView.SelectedItem != null)
            {
                actualItem =(SimpleObject)((TreeViewItem)unitsTreeView.SelectedItem).DataContext;
            }
            UpdatePanels(actualItem);

        }

        private void UpdatePanels(SimpleObject selectedObject)
        {
            bool scenarioPanel = false;
            bool sidePanel = false;
            bool armyPanel = false;
            bool corpsPanel = false;
            bool divisionPanel = false;
            bool brigadePanel = false;
            bool regimentPanel = false;

            if (selectedObject != null)
            {
                if(selectedObject is Scenario)
                {
                    scenarioPanel = true;
                    ScenarioDetailPanel.DataContext = selectedObject;
                }
                if (selectedObject is ScenarioSide)
                {
                    sidePanel = true;
                    SideDetailPanel.DataContext = selectedObject;
                }
                if (selectedObject is Army)
                {
                    armyPanel = true;
                    ArmyDetailPanel.DataContext = selectedObject;
                }
                if (selectedObject is Corps)
                {
                    corpsPanel = true;
                    CorpsDetailPanel.DataContext = selectedObject;
                }
                if (selectedObject is ArmyDivision)
                {
                    divisionPanel = true;
                    DivisionDetailPanel.DataContext = selectedObject;
                }
                if (selectedObject is Brigade)
                {
                    brigadePanel = true;
                    BrigadeDetailPanel.DataContext = selectedObject;
                }
                if (selectedObject is Regiment)
                {
                    regimentPanel = true;
                    RegimentDetailPanel.DataContext = selectedObject;
                }
            }

            this.ScenarioDetailPanel.Visibility = scenarioPanel ? Visibility.Visible : Visibility.Collapsed;
            this.SideDetailPanel.Visibility = sidePanel ? Visibility.Visible : Visibility.Collapsed;
            this.ArmyDetailPanel.Visibility = armyPanel ? Visibility.Visible : Visibility.Collapsed;
            this.CorpsDetailPanel.Visibility = corpsPanel ? Visibility.Visible : Visibility.Collapsed;
            this.DivisionDetailPanel.Visibility = divisionPanel ? Visibility.Visible : Visibility.Collapsed;
            this.BrigadeDetailPanel.Visibility = brigadePanel ? Visibility.Visible : Visibility.Collapsed;
            this.RegimentDetailPanel.Visibility = regimentPanel ? Visibility.Visible : Visibility.Collapsed;

        }
    }
}

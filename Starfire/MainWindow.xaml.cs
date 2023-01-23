using Starfire.Core.Network;
using Starfire.UI;
using System;
using System.Collections.Generic;
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

namespace Starfire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static StandaloneStarfireGame game;
        private System.Threading.Timer backgroundTimer;
        static readonly object _locker = new object();
        private static MapVisualHost mapVisualHost;
        private Controller controller;

        public MainWindow()
        {
            InitializeComponent();

            // create the game
            game = new StandaloneStarfireGame();
            //AmericanCivilWarGameCreator.CreateGame(game);
            controller = new Controller() { Game = game };


            // the visual host
            mapVisualHost = new MapVisualHost(game, controller);
            this.MainWindowCanvas.Children.Add(mapVisualHost);
            this.MainWindowCanvas.InvalidateVisual();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

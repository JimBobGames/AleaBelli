using AleaBelli.Core.Data;
using AleaBelli.Core.Hex;
using AleaBelli.Core.Network;
using AleaBelli.UI;
using AleaBelli.UI.GameRenderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using static AleaBelli.Core.Hex.HexMap;
using Point = AleaBelli.Core.Hex.Point;

namespace AleaBelli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static StandaloneAleaBelliGame game;
        private System.Threading.Timer backgroundTimer;
        static readonly object _locker = new object();
        private static MapVisualHost mapVisualHost;
        private Controller controller;

        public MainWindow()
        {
            InitializeComponent();

            // create the game
            game = new StandaloneAleaBelliGame();
            AmericanCivilWarGameCreator.CreateGame(game);
            controller = new Controller() { Game = game };


            // the visual host
            mapVisualHost = new MapVisualHost(game, controller);
            this.MapCanvas.Children.Add(mapVisualHost);
            this.MapCanvas.InvalidateVisual();


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += timer_Tick;
            timer.Start();

            //MapCanvas.MouseEnter += new MouseEventHandler(canvas_MouseEnter);
            MapCanvas.MouseWheel += new MouseWheelEventHandler(Canvas_MouseWheel);
            // MapCanvas.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonDown);

            this.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonDown);
            this.SizeChanged += OnWindowSizeChanged;


            var autoEvent = new AutoResetEvent(false);
            backgroundTimer = new System.Threading.Timer(BackgroundUpdate, autoEvent, 1000, 100);


            
            

        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // position detail screen
            double height = this.ActualHeight - 120.0;

            SelectedDetailCanvas.Visibility = Visibility.Hidden;
            //Canvas.s
            Canvas.SetTop(SelectedDetailCanvas, height);
            //SetTop(child, GetTop(child) / (double)e.OldValue * (double)e.NewValue);


        }

        // Respond to the left mouse button down event by initiating the hit test.
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // delegate to the visual host
            mapVisualHost.OnMouseLeftButtonDown(sender, e);

        }

        private static void BackgroundUpdate(Object stateInfo)
        {
            lock (_locker)
            {
                try
                {

                    AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
                    lastBackgroundUpdate = DateTime.Now;

                    game.UpdateGameStates(changes);

                   // mapVisualHost.Refresh();
                }
                catch(Exception ex)
                {

                }
                finally
                {

                }
            }


        }


        private void UpdateSelectionDisplay()
        {
            if (game.SelectedRegiment == null)
            {
                this.SelectedDetailCanvas.Visibility = Visibility.Hidden;
            }
            else
            {
                this.SelectedDetailCanvas.Visibility = Visibility.Visible;
            }
        }

        private static DateTime lastBackgroundUpdate = DateTime.Now;
        private static UIChanges changes = new UIChanges();

        /// <summary>
        /// The dispathed timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            lock (_locker)
            {
                try
                {
                    textBlock.Text = lastBackgroundUpdate.ToLongTimeString();

                    mapVisualHost.UpdateGameVisualsWithChangeList(changes);
                    changes = new UIChanges();
                    UpdateSelectionDisplay();
                    //mapVisualHost.UpdateGameVisuals();

                    //this.MapCanvas.InvalidateVisual();
                    //this.MapCanvas.UpdateLayout();

                    //mapVisualHost.Refresh();
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }


               
            }
        }

        const double ScaleRate = 1.1;
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                st.ScaleX *= ScaleRate;
                st.ScaleY *= ScaleRate;
            }
            else
            {
                st.ScaleX /= ScaleRate;
                st.ScaleY /= ScaleRate;
            }

            MapCanvas.InvalidateVisual();
        }


        private void MapCanvas_Initialized(object sender, EventArgs e)
        {

        }

        private void DrawPoint(Point p, Brush stroke)
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = stroke;
            //rect.Fill = new SolidColorBrush(Colors.Black);
            rect.Width = 2;
            rect.Height = 2;
            Canvas.SetLeft(rect, p.x);
            Canvas.SetTop(rect, p.y);
            this.MapCanvas.Children.Add(rect);
        }

        private void DrawPoint(Point p)
        {
            DrawPoint(p, new SolidColorBrush(Colors.Red));
        }

        private void DrawLine(Point start, Point end)
        {
            Line l = new Line();
            l.Stroke = Brushes.LightSteelBlue;

            l.X1 = start.x;
            l.X2 = end.x;
            l.Y1 = start.y;
            l.Y2 = end.y;

            l.StrokeThickness = 2;
            this.MapCanvas.Children.Add(l);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void SelectedDetailCanvas_Initialized(object sender, EventArgs e)
        {
        }

        //this.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonDown);

    }
}

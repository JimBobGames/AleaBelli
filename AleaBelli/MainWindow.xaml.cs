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

        public MainWindow()
        {
            InitializeComponent();

            // create the game
            game = new StandaloneAleaBelliGame();
            AmericanCivilWarGameCreator.CreateGame(game);

            // the visual host
            mapVisualHost = new MapVisualHost(game);
            this.MapCanvas.Children.Add(mapVisualHost);
            this.MapCanvas.InvalidateVisual();


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timer_Tick;
            timer.Start();

            //MapCanvas.MouseEnter += new MouseEventHandler(canvas_MouseEnter);
            MapCanvas.MouseWheel += new MouseWheelEventHandler(Canvas_MouseWheel);



            var autoEvent = new AutoResetEvent(false);
            backgroundTimer = new System.Threading.Timer(BackgroundUpdate, autoEvent, 1000, 100);


            
            

        }

        private static void BackgroundUpdate(Object stateInfo)
        {
            lock (_locker)
            {
                try
                {

                    AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
                    lastBackgroundUpdate = DateTime.Now;

                    game.UpdateGameStates();
                }
                catch(Exception ex)
                {

                }
                finally
                {

                }
            }


        }

        private static DateTime lastBackgroundUpdate = DateTime.Now;


        void timer_Tick(object sender, EventArgs e)
        {
            lock (_locker)
            {
                try
                {
                    textBlock.Text = lastBackgroundUpdate.ToLongTimeString();
                    mapVisualHost.UpdateGameVisuals();
                    this.MapCanvas.InvalidateVisual();
                    this.MapCanvas.UpdateLayout();
                    //this.MapCanvas.i
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

    }
}

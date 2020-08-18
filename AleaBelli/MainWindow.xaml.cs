using AleaBelli.Core.Hex;
using AleaBelli.Core.Network;
using AleaBelli.UI;
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

        public MainWindow()
        {
            InitializeComponent();

            game = new StandaloneAleaBelliGame();
            AmericanCivilWarGameCreator.CreateGame(game);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            //MapCanvas.MouseEnter += new MouseEventHandler(canvas_MouseEnter);
            MapCanvas.MouseWheel += new MouseWheelEventHandler(Canvas_MouseWheel);

            MapVisualHost mvh = new MapVisualHost(game);
            this.MapCanvas.Children.Add(mvh);
            this.MapCanvas.InvalidateVisual();

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
                    game.UpdateGameVisuals();
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

        private void Window_LoadedXXX(object sender, RoutedEventArgs e)
        {
            Line l = new Line();
            l.Stroke = Brushes.LightSteelBlue;

            l.X1 = 1;
            l.X2 = 50;
            l.Y1 = 1;
            l.Y2 = 50;

            l.StrokeThickness = 2;
            this.MapCanvas.Children.Add(l);

            HexMap map = game.HexMap;
            Layout layout = game.HexLayout;

            for (int r = 0; r < map.MapHeight; r++)
            {
                //int r_offset = (int) Math.Floor( (double) (r / 2)); // or r>>1
                int r_offset = map.GetRowOffset(r);

                for (int q = -r_offset; q < map.MapWidth - r_offset; q++)
                {
                    Point p = new Point(r, q);
                    Hex hex = map.GetHex(p);
                    MapHex maphex = new MapHex(hex, r, q + r_offset);
                    Point[] hexpoints = layout.PolygonCorners(hex).ToArray<Point>();

                    for(int i = 0; i<hexpoints.Length-1;i++)
                    {
                        l = new Line();
                        l.Stroke = Brushes.LightSteelBlue;

                        l.X1 = hexpoints[i].x;
                        l.X2 = hexpoints[i+1].x;
                        l.Y1 = hexpoints[i].y;
                        l.Y2 = hexpoints[i+1].y;

                        l.StrokeThickness = 2;
                        this.MapCanvas.Children.Add(l);


                    }
                    DrawLine(hexpoints[2], hexpoints[4]);
              


                    /*
                    l = new Line();
                    l.Stroke = Brushes.LightSteelBlue;

                    l.X1 = hexpoints[0].x;
                    l.X2 = hexpoints[1].x;
                    l.Y1 = hexpoints[0].y;
                    l.Y2 = hexpoints[1].y;

                    l.StrokeThickness = 2;
                    this.MapCanvas.Children.Add(l);
                    */
                }

                // get hex at coords
                Point hexp = new Point(100, 100);
                FractionalHex fh = layout.PixelToHex(hexp);
                
                DrawPoint(hexp);
                List<Point> hexpoints2 = layout.PolygonCorners(fh.HexRound());
                foreach (Point pt in hexpoints2)
                {
                    DrawPoint(pt);
                }
                DrawPoint(hexpoints2[0], Brushes.Green);
            }



            /*
            PointCollection points = new PointCollection();
            List<ManeBellum.Core.Util.Point> hexpoints = Layout.PolygonCorners(layout, hex);

            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                Point first = hexpoints[0];
                geometryContext.BeginFigure(new System.Windows.Point(first.x, first.y), true, true);
                for (int idx = 1; idx < hexpoints.Count; idx++)
                {
                    Point pt = hexpoints[idx];
                    points.Add(new System.Windows.Point(pt.x, pt.y));
                }
                geometryContext.PolyLineTo(points, true, true);
            }


            Pen pen = new Pen(Brushes.Blue, 1.0);
            dc.DrawGeometry(Brushes.Black, pen, streamGeometry);
            */

        }

    }
}

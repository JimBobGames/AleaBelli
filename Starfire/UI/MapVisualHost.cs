using Starfire.Core.Data;
using Starfire.Core.Hex;
using Starfire.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static Starfire.Core.Hex.HexMap;

namespace Starfire.UI
{
    /// <summary>
    /// https://www.c-sharpcorner.com/UploadFile/393ac5/frameworkelement-class-in-wpf/
    /// 
    /// https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/optimizing-performance-2d-graphics-and-imaging
    /// 
    /// hit testing
    /// https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/using-drawingvisual-objects
    /// </summary>
    public class MapVisualHost : FrameworkElement
    {
        private List<Visual> m_Visuals = new List<Visual>();
        private IStarfireGame game;
        private Controller controller;
        private Dictionary<DrawingVisual, Counter> counterVisuals = new Dictionary<DrawingVisual, Counter>();
        private Dictionary<DrawingVisual, MapHex> hexVisuals = new Dictionary<DrawingVisual, MapHex>();
        private Dictionary<int, DrawingVisual> counterVisualsById = new Dictionary<int, DrawingVisual>();
        private HexMap hexMap;
        private Layout layout;

        public MapVisualHost(IStarfireGame game, Controller controller, HexMap hexMap)
        {
            this.game = game;
            this.ClipToBounds = false;
            this.controller = controller;
            this.hexMap = hexMap;
            layout = new Layout(Layout.flat, new Core.Hex.Point(40,40), new Core.Hex.Point(0,0));

            UpdateGameVisuals();

            // Add the event handler for MouseLeftButtonUp.
            ///            this.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonDown);


        }

        protected override int VisualChildrenCount
        {
            get { return m_Visuals.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= m_Visuals.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return m_Visuals[index];
        }

        public void OnMapHex(MapHex mh)
        {
            DrawHex(mh);
        }

    /// <summary>
    /// Runs in the game UI Thread
    /// </summary>
        public void UpdateGameVisuals()
        {
            // Remove all Visuals
            m_Visuals.ForEach(delegate (Visual v) { RemoveVisualChild(v); });
            m_Visuals.Clear();

            counterVisuals.Clear();
            hexVisuals.Clear();

            // draw the hex map
            hexMap.AllHexesInMapCoords(OnMapHex);

            //foreach (var h in hexMap.Hexes())
            //{
            //    DrawHex(h);
            //}

            Counter c = new Counter();
            c.MapX = 10;
            //RedrawCounter(c, false);
           
            
            /*
                        // update the regiments if required
                        foreach (Army a in game.Armies.Values)
                        {
                            foreach (Corps c in a.Corps)
                            {
                                foreach (ArmyDivision d in c.Divisons)
                                {
                                    foreach (Brigade b in d.Brigades)
                                    {
                                        RedrawBrigade(b, false);
                                        foreach (Regiment r in b.Regiments)
                                        {
                                            RedrawRegiment(r, false);
                                        }
                                    }
                                }

                            }
                        }
            */


        }

        private void DrawHex(MapHex mh)
        {
            // render a new visual
            DrawingVisual dv = CreateHexDrawingVisual(mh);
            hexVisuals[dv] = mh;
            m_Visuals.Add(dv);
            AddVisualChild(dv);
        }

        private DrawingVisual CreateHexDrawingVisual(MapHex mh)
        {
            List<Core.Hex.Point> corners = layout.PolygonCorners(mh.h);



            int width = 5;
            int height = 5;

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                foreach (Core.Hex.Point p in corners)
                {
                    //DrawCounter(dc, c);
                    Rect rect = new Rect(new System.Windows.Point(p.x, p.y), new System.Windows.Size(width, height));
                    dc.DrawRectangle(System.Windows.Media.Brushes.Red, (System.Windows.Media.Pen)null, rect);
                }
            }
            return drawingVisual;
        }


        public void RedrawCounter(Counter c, bool removeOldVisuals)
        {
            if (c != null)
            {
                if (removeOldVisuals)
                {
                    // remove the old visual if it exists
                    DrawingVisual oldVisual = null;
                    counterVisualsById.TryGetValue(c.CounterId, out oldVisual);
                    if (oldVisual != null)
                    {
                        RemoveVisualChild(oldVisual);
                        m_Visuals.Remove(oldVisual);
                    }
                }

                // render a new visual
                DrawingVisual dv = CreateCounterDrawingVisual(c);
                counterVisuals[dv] = c;
                counterVisualsById[c.CounterId] = dv;
                m_Visuals.Add(dv);
                AddVisualChild(dv);
            }
        }

        private DrawingVisual CreateCounterDrawingVisual(Counter c)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                DrawCounter(dc, c);
            }
            return drawingVisual;
        }


        private void DrawCounter(DrawingContext dc, Counter r)
        {
            int width = 100;  //r.GetWidthInPaces();
            int halfwidth = width / 2;
            int height = 100; // r.GetDepthInPaces();

            //int x = 100;
            //int y = 100;
            int x = r.MapX;
            int y = r.MapY;
            int angle = r.FacingInDegrees;

            r.ShortName = DateTime.Now.ToLongTimeString();

            dc.PushTransform(new RotateTransform(angle, x + halfwidth, y));

            // Create a rectangle and draw it in the DrawingContext.
            Rect rect = new Rect(new System.Windows.Point(x, y), new System.Windows.Size(width, height));
            dc.DrawRectangle(System.Windows.Media.Brushes.Blue, (System.Windows.Media.Pen)null, rect);

            //Rect rect2 = new Rect(new System.Windows.Point(x, y + height), new System.Windows.Size(width, 10));
            //dc.DrawRectangle(System.Windows.Media.Brushes.Black, (System.Windows.Media.Pen)null, rect2);

            /*
            Rect rect3 = new Rect(new System.Windows.Point(x + (halfwidth - widgetsize) //150
            , y - widgetsize), new System.Windows.Size(20, 10));
            dc.DrawRectangle(System.Windows.Media.Brushes.Red, (System.Windows.Media.Pen)null, rect3);
            */

            dc.DrawText(


           new FormattedText(r.ShortName,
              System.Globalization.CultureInfo.GetCultureInfo("en-us"),
              FlowDirection.LeftToRight,
              new Typeface("Verdana"),
              12, System.Windows.Media.Brushes.Black),
              new System.Windows.Point(x, y + height));



            dc.Pop();
        }


    }
}

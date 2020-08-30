using AleaBelli.Core.Data;
using AleaBelli.Core.Network;
using System;
using System.Collections.Generic;
using System.Globalization;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AleaBelli.UI
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
    private IAleaBelliGame game = null;
    private Dictionary<DrawingVisual, Regiment> regimentVisuals = new Dictionary<DrawingVisual, Regiment>();
    private Dictionary<int, DrawingVisual> regimentVisualsById = new Dictionary<int, DrawingVisual>();
    private Controller controller;

    public MapVisualHost(IAleaBelliGame game, Controller controller)
    {
        this.game = game;
        this.ClipToBounds = false;
        this.controller = controller;

            //DrawScreen();

            //CreateMapVisuals();
            UpdateGameVisuals();
            //m_Visuals.Add(CreateDrawingVisualRectangle());

            // Add the event handler for MouseLeftButtonUp.
            this.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonDown);


        }


        // Respond to the left mouse button down event by initiating the hit test.
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the coordinate of the mouse position.
            System.Windows.Point pt = e.GetPosition((UIElement)sender);

            // Initiate the hit test by setting up a hit test result callback method.
            VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(HitTestCallBack), new PointHitTestParameters(pt));
        }

        // If a child visual object is hit, toggle its opacity to visually indicate a hit.
        public HitTestResultBehavior HitTestCallBack(HitTestResult result)
        {
            if (result.VisualHit.GetType() == typeof(DrawingVisual))
            {
                DrawingVisual visualHit = (DrawingVisual)result.VisualHit;

                // is this a regiment ??
                if (regimentVisuals.ContainsKey(visualHit))
                {
                    controller.OnClickRegiment(regimentVisuals[visualHit]);
                }

                if (((DrawingVisual)result.VisualHit).Opacity == 1.0)
                {
                    ((DrawingVisual)result.VisualHit).Opacity = 0.4;
                }
                else
                {
                    ((DrawingVisual)result.VisualHit).Opacity = 1.0;
                }
            }

            // Stop the hit test enumeration of objects in the visual tree.
            return HitTestResultBehavior.Stop;
        }


        /*
        public void DrawScreen(IAleaBelliGame client)
        {
            // Remove all Visuals
            m_Visuals.ForEach(delegate (Visual v) { RemoveVisualChild(v); });
            m_Visuals.Clear();

            //m_Visuals.Add(CreateDrawingVisualRectangle(client));

            m_Visuals.ForEach(delegate (Visual v) { AddVisualChild(v); });
        }*/
    
        public void UpdateGameVisualsWithChangeList(UIChanges changes)
        {
            if (changes != null)
            {
                // redraw any regimentd
                foreach (int rid in changes.RegimentalIds.ToList())
                {
                    Regiment r = game.GetRegiment(rid);
                    RedrawRegiment(r, true);
                }
            }
        }


        public void RedrawRegiment(Regiment r, bool removeOldVisuals)
        {
            if (r != null)
            {
                if (removeOldVisuals)
                {
                    // remove the old visual if it exists
                    DrawingVisual oldVisual = null;
                    regimentVisualsById.TryGetValue(r.RegimentId, out oldVisual);
                    if (oldVisual != null)
                    {
                        RemoveVisualChild(oldVisual);
                        m_Visuals.Remove(oldVisual);
                    }
                }

                // render a new visual
                DrawingVisual dv = CreateRegimentalDrawingVisual(r);
                regimentVisuals[dv] = r;
                regimentVisualsById[r.RegimentId] = dv;
                m_Visuals.Add(dv);
                AddVisualChild(dv);
            }
        }



        /// <summary>
        /// Runs in the game UI Thread
        /// </summary>
        public void UpdateGameVisuals()
        {
            // Remove all Visuals
            m_Visuals.ForEach(delegate (Visual v) { RemoveVisualChild(v); });
            m_Visuals.Clear();

            regimentVisuals.Clear();

            // update the regiments if required
            foreach (Army a in game.Armies.Values)
            {
                foreach (Corps c in a.Corps)
                {
                    foreach (ArmyDivision d in c.Divisons)
                    {

                        foreach (Brigade b in d.Brigades)
                        {
                            foreach (Regiment r in b.Regiments)
                            {
                                RedrawRegiment(r, false);
                                /*
                                DrawingVisual dv = CreateRegimentalDrawingVisual(r);
                                //dv.
                                    regimentVisuals[dv] = r;
                                    regimentVisualsById[r.RegimentId] = dv;
                                    m_Visuals.Add(dv);
                                    AddVisualChild(dv);
                                    */
                            }
                        }
                    }

                }
            }




        }

        private void DrawRegiment(DrawingContext dc, Regiment r)
        {
            int width = r.GetWidthInPaces();
            int halfwidth = width / 2;
            int height = r.GetDepthInPaces();

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
              CultureInfo.GetCultureInfo("en-us"),
              FlowDirection.LeftToRight,
              new Typeface("Verdana"),
              12, System.Windows.Media.Brushes.Black),
              new System.Windows.Point(x, y + height));



            dc.Pop();
        }


        private DrawingVisual CreateRegimentalDrawingVisual(Regiment r)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                DrawRegiment(dc, r);

                //Rect rect = new Rect(new System.Windows.Point(50, 50), new System.Windows.Size(100, 100));
                //dc.DrawRectangle(System.Windows.Media.Brushes.Blue, (System.Windows.Media.Pen)null, rect);


                //DrawRegiment(dc, 120, 120, 45, new Battalion() { ShortName = "1st NY" });
                // DrawRegiment(dc, 220, 160, 55, new Battalion() { ShortName = "2nd NY" });
                // DrawRegiment(dc, 320, 220, 65, new Battalion() { ShortName = "3rd NY" });

            }
            return drawingVisual;
        }



        class Battalion
    {
        public string ShortName { get; internal set; }

        internal int GetWidthInPaces()
        {
            return 100;
        }

        internal int GetDepthInPaces()
        {
            return 15;
        }
    }

    private void DrawRegiment(DrawingContext dc, int x, int y, int angle, Battalion bn)
    {
        int width = bn.GetWidthInPaces();
        int halfwidth = width / 2;
        int height = bn.GetDepthInPaces();

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
       
            
       new FormattedText(bn.ShortName,
          CultureInfo.GetCultureInfo("en-us"),
          FlowDirection.LeftToRight,
          new Typeface("Verdana"),
          12, System.Windows.Media.Brushes.Black),
          new System.Windows.Point(x, y + height));
         


        dc.Pop();
    }



    private DrawingVisual CreateDrawingVisualRectangle()
    {
        DrawingVisual drawingVisual = new DrawingVisual();
        //drawingVisual.Transform = new TranslateTransform(100, 100 + offsety);

        // Retrieve the DrawingContext in order to create new drawing content.
        using (DrawingContext dc = drawingVisual.RenderOpen())
        {
            //Rect rect = new Rect(new System.Windows.Point(50, 50), new System.Windows.Size(100, 100));
            //dc.DrawRectangle(System.Windows.Media.Brushes.Blue, (System.Windows.Media.Pen)null, rect);


            DrawRegiment(dc, 120, 120, 45, new Battalion() { ShortName = "1st NY" });
            DrawRegiment(dc, 220, 160, 55, new Battalion() { ShortName = "2nd NY" });
            DrawRegiment(dc, 320, 220, 65, new Battalion() { ShortName = "3rd NY" });

        }


        // Persist the drawing content.
        //drawingContext.Close();



        return drawingVisual;
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
}
}

        /*
    public void DrawScreen(ManeBellumWPFClientGame client)
    {
        // Remove all Visuals
        m_Visuals.ForEach(delegate (Visual v) { RemoveVisualChild(v); });
        m_Visuals.Clear();

        m_Visuals.Add(CreateDrawingVisualRectangle(client));

        m_Visuals.ForEach(delegate (Visual v) { AddVisualChild(v); });
    }



    /// <summary>
    /// https://www.codeproject.com/Articles/33308/Draw-a-Boardgame-in-WPF
    /// </summary>

    int offsety = 1;

    /// <returns></returns>
    // Create a DrawingVisual that contains a rectangle.
    private DrawingVisual CreateDrawingVisualRectangle(ManeBellumWPFClientGame client)
    {
        DrawingVisual drawingVisual = new DrawingVisual();
        //drawingVisual.Transform = new TranslateTransform(100, 100 + offsety);

        // Retrieve the DrawingContext in order to create new drawing content.
        using (DrawingContext dc = drawingVisual.RenderOpen())
        {


            offsety++;
            //DrawRegiment(dc, 100, 100, 0);
            //DrawRegiment(dc, 390, 110, 1);
            //DrawRegiment(dc, 690, 180, angle);

            foreach (Battalion bn in client.Battalions.Values)
            {
                DrawRegiment(dc, bn.MapX, bn.MapY, bn.MapRotation, bn);
            }

        }


        // Persist the drawing content.
        //drawingContext.Close();



        return drawingVisual;
    }

    //private int height = 80;
    //private int width = 320;
    ///private int halfwidth = 160;
    int widgetsize = 10;

    private void DrawRegiment(DrawingContext dc, int x, int y, int angle, Battalion bn)
    {
        int width = bn.GetWidthInPaces();
        int halfwidth = width / 2;
        int height = bn.GetDepthInPaces();

        dc.PushTransform(new RotateTransform(angle, x + halfwidth, y));

        // Create a rectangle and draw it in the DrawingContext.
        Rect rect = new Rect(new System.Windows.Point(x, y), new System.Windows.Size(width, height));
        dc.DrawRectangle(System.Windows.Media.Brushes.Red, (System.Windows.Media.Pen)null, rect);

        Rect rect2 = new Rect(new System.Windows.Point(x, y + height), new System.Windows.Size(width, 10));
        dc.DrawRectangle(System.Windows.Media.Brushes.Black, (System.Windows.Media.Pen)null, rect2);

        Rect rect3 = new Rect(new System.Windows.Point(x + (halfwidth - widgetsize)  // 150
        , y - widgetsize), new System.Windows.Size(20, 10));
        dc.DrawRectangle(System.Windows.Media.Brushes.Red, (System.Windows.Media.Pen)null, rect3);

        dc.DrawText(
       new FormattedText(bn.ShortName,
          CultureInfo.GetCultureInfo("en-us"),
          FlowDirection.LeftToRight,
          new Typeface("Verdana"),
          36, System.Windows.Media.Brushes.Black),
          new System.Windows.Point(x, y));

        dc.Pop();
    }
}
}
*/
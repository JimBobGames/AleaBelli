using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using System.Windows.Media;
using AleaBelli.Core.Hex;
using AleaBelli.Core.Network;
using static AleaBelli.Core.Hex.HexMap;

namespace AleaBelli.UI.GameRenderer
{
    public class TiledMapRenderer2
    {
        public void DrawTiledMap(TmxMap map)
        {
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;

            //int tilesetTilesWide = tileset.Width / tileWidth;
            //int tilesetTilesHigh = tileset.Height / tileHeight;

            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
                    int tileFrame = gid - 1;
                    //int column = tileFrame % tilesetTilesWide;
                    //int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
/*
                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    */
                }
            }
        }
    }

    public class TiledMapRenderer
    {
        private static Layout flat = new Layout(Layout.flat, new Point(10.0, 15.0), new Point(35.0, 71.0));
        //private static Layout pointy = new Layout(Layout.pointy, new Point(10.0, 15.0), new Point(35.0, 71.0));

        private static Layout pointy = new Layout(Layout.pointy, new Point(36.0, 36.0), new Point(34, 10));
        private static Layout hexLayout = pointy;
        //EqualHex("layout", h, pointy.PixelToHex(pointy.HexToPixel(h)).HexRound());

        public void DrawTiledMap(TmxMap map)
        {

        }

        // cache of stream geometry
        private Dictionary<Point, StreamGeometry> geometryCache = new Dictionary<Point, StreamGeometry>();

        /// <summary>TODO</summary>
        /// <param name="dc"></param>
        public void OnRender(DrawingContext dc, IAleaBelliGame game)
        {
            if (dc == null) throw new ArgumentNullException("dc");
            //if (game == null) throw new ArgumentNullException("game");

            if (game != null)
            {
                DateTime start = DateTime.Now;
                RenderTerrain(dc, game);
                DateTime stop = DateTime.Now;
                TimeSpan duration = stop - start;
                double totalms = duration.TotalMilliseconds;
                Console.WriteLine("MapRenderer::OnRender  " + totalms + " ms");
            }
        }


        // Create a method for a delegate.
        public void RenderMapCallback(MapHex mh, DrawingContext dc, IAleaBelliGame game)
        {
            Pen pen = new Pen(Brushes.Green, 1.0);

            // have we already cached this ??
            StreamGeometry streamGeometry;
            if (!geometryCache.TryGetValue(mh.p, out streamGeometry))
            {
                List<Point> hexpoints = hexLayout.PolygonCorners(mh.h);
                streamGeometry = new StreamGeometry();
                PointCollection points = new PointCollection();
                using (StreamGeometryContext geometryContext = streamGeometry.Open())
                {
                    Point first = hexpoints[0];
                    geometryContext.BeginFigure(new System.Windows.Point(first.x, first.y), true, true);
                    for (int idx = 1; idx < hexpoints.Count; idx++)
                    {
                        Point pt = hexpoints[idx];
                        points.Add(new System.Windows.Point(pt.x, pt.y));

                        // dc.DrawRectangle(Brushes.Red, pen, new System.Windows.Rect(pt.x, pt.y, 2, 2));

                    }
                    geometryContext.PolyLineTo(points, true, true);

                    geometryContext.Close();

                }
                streamGeometry.Freeze();

                geometryCache[mh.p] = streamGeometry;
            }



            //Pen pen = new Pen(Brushes.Green, 1.0);
            if (game.RenderHexGrid)
            {
                dc.DrawGeometry(Brushes.Blue, pen, streamGeometry);
            }
            //Console.WriteLine("MapRenderer::RenderMapCallback " + mh.ToString());
        }

            private void RenderTerrain(DrawingContext dc, IAleaBelliGame game)
            {
                // for each map hex render it
                game.HexMap.AllHexesInMapCoords((maphex) => { RenderMapCallback(maphex, dc, game); });



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



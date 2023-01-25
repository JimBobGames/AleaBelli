using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starfire.Core.Hex
{
    public class HexMap
    {
        private Dictionary<Point, Hex> map = new Dictionary<Point, Hex>();

        private HexMap()
        {

        }

        public int MapHeight { get; set; }
        public int MapWidth { get; set; }

        public int Count()
        {
            return map.Count;
        }

        public List<Hex> Hexes()
        {
            return map.Values.ToList<Hex>();
        }

        public Hex GetHex(Point p)
        {
            Hex h;
            map.TryGetValue(p, out h);
            return h;
        }

        public Hex GetHex(int r, int q)
        {
            Point p = new Point(r, q);
            return GetHex(p);
        }

        public int GetRowOffset(int r)
        {
            return (int)Math.Floor((double)(r / 2)); // or r>>1
        }

        /// <summary>
        /// This creates a 'rectangular' map
        /// </summary>
        /// <param name="map_height"></param>
        /// <param name="map_width"></param>
        /// <returns></returns>
        public static HexMap CreateMap(int map_height, int map_width)
        {
            HexMap hexmap = new HexMap();

            hexmap.MapHeight = map_height;
            hexmap.MapWidth = map_width;

            for (int r = 0; r < map_height; r++)
            {
                //int r_offset = (int) Math.Floor( (double) (r / 2)); // or r>>1
                int r_offset = hexmap.GetRowOffset(r);

                for (int q = -r_offset; q < map_width - r_offset; q++)
                {
                    Point p = new Point(r, q);

                    hexmap.map[p] = new Hex(q, r, -q - r);
                }
            }

            return hexmap;
        }

        public struct MapHex
        {
            public MapHex(Hex h, int x, int y)
            {
                this.h = h;
                this.p = new Point(x, y);
            }

            public readonly Hex h;
            public readonly Point p;

        }

        public delegate void OnMapHex(MapHex mh);

        /// <summary>
        /// Loops thru all the hexes 'hiding' the complexity
        /// </summary>
        /// <param name="callback"></param>
        public void AllHexesInMapCoords(OnMapHex callback)
        {
            for (int r = 0; r < MapHeight; r++)
            {
                //int r_offset = (int) Math.Floor( (double) (r / 2)); // or r>>1
                int r_offset = GetRowOffset(r);

                for (int q = -r_offset; q < MapWidth - r_offset; q++)
                {
                    Point p = new Point(r, q);
                    MapHex maphex = new MapHex(map[p], r, q + r_offset);
                    callback(maphex);
                }
            }


        }

    }
}

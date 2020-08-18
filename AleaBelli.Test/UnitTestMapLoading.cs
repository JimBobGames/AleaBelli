using System;
using System.Collections.Generic;
using AleaBelli.Core.Hex;
using AleaBelli.Core.Network;
using AleaBelli.Core.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiledSharp;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestMapLoading
    {
        private PersistenceManager persistenceManager = new PersistenceManager();

        [TestMethod]
        public void TestLoadMap()
        {
            TmxMap map = persistenceManager.LoadTiledMap("Maps/testmap.tmx");
            string version = map.Version;
            Assert.IsNotNull(version);
            int height = map.Height;
            int width = map.Width;

            // foreach()
        }

        [TestMethod]
        public void TestHexDetails()
        {
            StandaloneAleaBelliGame game = new StandaloneAleaBelliGame();
            AmericanCivilWarGameCreator.CreateGame(game);
            HexMap map = game.HexMap;
            Assert.IsNotNull(game.HexMap);

            Layout hexLayout = game.HexLayout;
            Assert.IsNotNull(hexLayout);

            Hex h = map.GetHex(1,1);
            Assert.IsNotNull(h);

            Console.WriteLine("hex " + hexLayout.size.x + " " + hexLayout.size.y);

            List<Point> hexpoints = hexLayout.PolygonCorners(h);

            double minx = 0, miny = 0, maxx = 0, maxy = 0; ;
            minx = hexpoints.ToArray()[0].x;
            miny = hexpoints.ToArray()[0].y;
            maxx = hexpoints.ToArray()[0].x;
            maxx = hexpoints.ToArray()[0].y;

            foreach (Point p in hexpoints)
            {
                Console.WriteLine("p " + p.x + " " + p.y);

                if (p.x < minx)
                {
                    minx = p.x;
                }
                if (p.y < miny)
                {
                    miny = p.y;
                }
                if (p.x > maxx)
                {
                    maxx = p.x;
                }
                if (p.y > maxy)
                {
                    maxy = p.y;
                }
            }
            Console.WriteLine("minx + " + minx + ", maxx " + maxx);
            Console.WriteLine("miny + " + miny + ", maxy " + maxy);
            Console.WriteLine("diffx + " + (maxx-minx) + ", diffy " + (maxy-miny));

            //hexLayout.
            //Console.WriteLine("hex height " + h.)
        }
    }
}
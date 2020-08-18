using AleaBelli.Core.Data;
using AleaBelli.Core.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Network
{
    public class GameCreator
    {
        public const int NATION_UK = 10;
        public const int NATION_FRANCE = 11;
        public const int NATION_RUSSIA = 12;
        public const int NATION_ITALY = 13;
        public const int NATION_GERMANY = 14;
        public const int NATION_AUSTRIA_HUNGARY = 15;
        public const int NATION_BULGARIA = 16;
        public const int NATION_SPAIN = 17;
        public const int NATION_USA = 18;
        public const int NATION_ROMANIA = 19;
        public const int NATION_SERBIA = 20;

        public const int SEAZONE_MED = 100;

        public const int PORT_MALTA = 1000;
        public const int PORT_TOULOUSE = 1001;

    }

    public class AmericanCivilWarGameCreator : GameCreator
    {
        /// <summary>
        /// https://latitude.to/map/fr/france/cities/toulouse
        /// </summary>
        /// <param name="g"></param>
        public static void CreateGame(AleaBelliGame g)
        {
            //g.HexLayout = new Layout(Layout.flat, new Point(10.0, 15.0), new Point(35.0, 71.0));
            //g.HexLayout = new Layout(Layout.flat, new Point(20.0, 30.0), new Point(10, 0));
            g.HexLayout = new Layout(Layout.flat, new Point(30.0, 35.0), new Point(10, 0));

            // 120 140

            g.HexMap = HexMap.CreateMap(40, 40);

            // default values
            g.RenderHexGrid = true;

            // add dummy player
            Player p = new Player() { PlayerId = 1, Name = "Local", Description = "Local Player" };
            g.AddPlayer(p);

            // add static sea zone data
            //SeaZoneStaticData medSeaZone = new SeaZoneStaticData() { Id = SEAZONE_MED, Name = "Med", Description = "Mediterranean Sea Zone", ImageName = "med.png" };
            //g.AddSeaZone(medSeaZone);



            // Valetta 35.89972 14.51472
            //MapLocation malta = new MapLocation() { Description = "Malta", Name = "Malta", Id = PORT_MALTA, Type = MapLocationType.Port, Location = new GeoCoordinate(35.89972, 14.51472), InitialOwner = NATION_UK };
            //medSeaZone.AddMapLocation(malta);

            // toulouse 43.60426 1.44367
           // MapLocation toulouse = new MapLocation() { Description = "Toulouse", Name = "Toulouse", Id = PORT_TOULOUSE, Type = MapLocationType.Port, Location = new GeoCoordinate(43.60426, 1.44367), InitialOwner = NATION_FRANCE };
           // medSeaZone.AddMapLocation(toulouse);


            // set the visible sea zone
//            g.VisibleSeaZone = 100;

        }
    }

}

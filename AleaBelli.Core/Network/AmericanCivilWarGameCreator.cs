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
        public const int NATION_CSA = 21;

        public const int ARMY_USA_POTOMAC = 1;
    }

    public class AmericanCivilWarGameCreator : GameCreator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public static void CreateGame(AleaBelliGame g)
        {
            // create the local player
            g.AddPlayer( new Player() { PlayerId = 1, Name = "Player"} );

            // create the nations
            g.AddNation(new Nation() { NationId = NATION_USA, Name = "Union" });
            g.AddNation(new Nation() { NationId = NATION_CSA, Name = "Conferderacy" });

            // create an army
            Army armyOfThePotomac = g.AddArmy(new Army() {ArmyId= ARMY_USA_POTOMAC, Name="Army Of The Potomac" });

            Corps firstCorps = armyOfThePotomac.AddCorps(new Corps() {CorpId=1,Name="First Corps" });

            ArmyDivision firstDivision = firstCorps.AddDivision(new ArmyDivision() {DivisionId=1, Name="1st Division" });

            Brigade brigade1 = firstDivision.AddBrigade( new Brigade() {BrigadeId=1, Name="Iron Brigade" });

            Regiment r1 = brigade1.AddRegiment( 
                new Regiment() {RegimentId=1, Name="1st New York", ShortName = "1st NY", RegimentFormation=RegimentFormation.Line2, FacingInDegrees=45 });
        }
    }

}

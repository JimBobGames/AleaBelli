using AleaBelli.Core.Data;
using AleaBelli.Core.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestScenario
    {
        [TestMethod]
        public void TestBlenheim()
        {
            // https://en.wikipedia.org/wiki/Blenheim_order_of_battle
            // http://www.spanishsuccession.nl/weapons.html
            Scenario blenheim = new Scenario()
            {
                Name = "Blenheim",
                ShortName = "Blenheim",
                Description = "",
                Date = new DateTime(1704, 08, 13, 08, 00, 0)
            };
            blenheim.TilemapName = @"Maps\terrain.tmx";

            /// list of officers


            ScenarioSide grandAlliance = new ScenarioSide() { Name = "Grand Alliance" };
            blenheim.AddSide(grandAlliance);

            Army alliedArmy = new Army() { Name = "Allied Army" };
            grandAlliance.AddArmy(alliedArmy);
            alliedArmy.Officer = new Officer() { Name = "John Churchill, 1st Duke of Marlborough", OfficerRank = OfficerRank.MajorGeneral };

            Corps blenheimCorps = new Corps() { Name = "Blenheim Column" };
            blenheimCorps.Officer = new Officer() { Name = "John Churchill, 1st Duke of Marlborough", OfficerRank = OfficerRank.MajorGeneral };
            alliedArmy.AddCorps(blenheimCorps);

            ArmyDivision infantryDivision = new ArmyDivision { Name = "Infantry" };
            infantryDivision.Officer = new Officer() { Name = "John, Lord Cutts", OfficerRank = OfficerRank.MajorGeneral };
            blenheimCorps.AddDivision(infantryDivision);

            Brigade rowesBrigade = new Brigade() { Name = "Rowe's Brigade"};
            rowesBrigade.Officer = new Officer() { Name = "Archibald Rowe", OfficerRank= OfficerRank.BrigadierGeneral };
            infantryDivision.AddBrigade(rowesBrigade);

            Regiment howesRegiment = new Regiment() { Name = "Howe's Regiment of Foot" };
            howesRegiment.Officer = new Officer() { Name = "Emanuel Scrope Howe", OfficerRank = OfficerRank.BrigadierGeneral };
            howesRegiment.Men = 584;
            howesRegiment.RegimentType = RegimentType.LineInfantry;
            rowesBrigade.AddRegiment(howesRegiment);

            Regiment welshRegiment = new Regiment() { Name = "The Welch Regiment of Fusilier" };
            welshRegiment.Officer = new Officer() { Name = "Richard Ingoldsby", OfficerRank = OfficerRank.BrigadierGeneral };
            welshRegiment.Men = 520;
            welshRegiment.RegimentType = RegimentType.LineInfantry;
            rowesBrigade.AddRegiment(welshRegiment);

            Regiment dukesRegiment = new Regiment() { Name = "The Duke of Marlborough's Regiment of Foot" };
            dukesRegiment.Officer = new Officer() { Name = "John Churchill", OfficerRank = OfficerRank.MajorGeneral };
            dukesRegiment.Men = 524;
            dukesRegiment.RegimentType = RegimentType.LineInfantry;
            rowesBrigade.AddRegiment(dukesRegiment);

            Regiment scotsRegiment = new Regiment() { Name = "Scots Fusiliers(21st Foot)" };
            scotsRegiment.Officer = new Officer() { Name = "Archibald Rowe", OfficerRank = OfficerRank.MajorGeneral };
            scotsRegiment.Men = 629;
            scotsRegiment.RegimentType = RegimentType.LineInfantry;
            rowesBrigade.AddRegiment(scotsRegiment);

            Regiment lordsRegiment = new Regiment() { Name = "Lord North and Grey's Regiment of Foot" };
            lordsRegiment.Officer = new Officer() { Name = "William North, 6th Baron North & 2nd Baron Grey", OfficerRank = OfficerRank.Colonel };
            lordsRegiment.Men = 580;
            lordsRegiment.RegimentType = RegimentType.LineInfantry;
            rowesBrigade.AddRegiment(lordsRegiment);


            Corps centerCorps = new Corps() { Name = "Centre" };
            centerCorps.Officer = new Officer() { Name = "Charles Churchill", OfficerRank = OfficerRank.MajorGeneral };
            alliedArmy.AddCorps(centerCorps);

            Corps austrianCorps = new Corps() { Name = "Army of Imperial Austria" };
            alliedArmy.AddCorps(austrianCorps);
            austrianCorps.Officer = new Officer() { Name = "Prince François Eugène von Savoy-Carignan", OfficerRank = OfficerRank.MajorGeneral };


            ScenarioSide french = new ScenarioSide() { Name = "French" };
            blenheim.AddSide(french);

            Army frenchArmy = new Army() { Name = "French Army" };
            french.AddArmy(frenchArmy);
            frenchArmy.Officer = new Officer() { Name = "Duke of Tallard", OfficerRank = OfficerRank.MajorGeneral };

            Army bavarianArmy = new Army() { Name = "Bavarian Army" };
            french.AddArmy(bavarianArmy);
            bavarianArmy.Officer = new Officer() { Name = "Maximilian II Emanuel", OfficerRank = OfficerRank.MajorGeneral };

            // save the file
            new PersistenceManager().SaveScenario(blenheim, "blenheim.scn");

        }

        [TestMethod]
        public void TestFirstBullRun()
        {
            // create the scenario programatically
            Scenario firstBullRun = new Scenario() { Name = "First Bull Run", ShortName = "First Bull Run", Description = "",
                Date = new DateTime(1861, 07, 21, 08, 00, 0) };
            firstBullRun.TilemapName = @"Maps\terrain.tmx";

            ScenarioSide union = new ScenarioSide() { Name = "Union" };
            firstBullRun.AddSide(union);
            ScenarioSide confederacy = new ScenarioSide() { Name = "Confederacy" };
            firstBullRun.AddSide(confederacy);

            Army aofNEV = new Army() { Name = "Army of Northeastern Virginia" };
            union.AddArmy(aofNEV);
            aofNEV.Officer = new Officer() { Name = "Irvin McDowell", OfficerRank = OfficerRank.MajorGeneral };

            Corps unionFirstCorps = new Corps() { Name = "1st Corps" };
            aofNEV.AddCorps(unionFirstCorps);

/*
 1st Division of Brig. Gen. Daniel Tyler the largest in the army, contained four brigades, led by Brig. Gen. Robert C. Schenck, Col. Erasmus D. Keyes, Col. William T. Sherman, and Col. Israel B. Richardson;
2nd Division of Col. David Hunter of two brigades. These were led by Cols. Andrew Porter and Ambrose E. Burnside;
3rd Division of Col. Samuel P. Heintzelman included 3 brigades, led by Cols. William B. Franklin, Orlando B. Willcox, and Oliver O. Howard;
4th Division of Brig. Gen. Theodore Runyon without brigade organization and not engaged, contained seven regiments of New Jersey and one regiment of New York volunteer infantries;
5th Division of Col. Dixon S. Miles included 2 brigades, commanded by Cols. Louis Blenker and Thomas A. Davies;
*/

            ArmyDivision unionFirstDivision = new ArmyDivision() { Name = "1st Division"};
            unionFirstCorps.AddDivision(unionFirstDivision);
            unionFirstDivision.Officer = new Officer() { Name = "Daniel Tyler", OfficerRank = OfficerRank.BrigadierGeneral };

            Brigade unionFirstBrigade = new Brigade() { Name = "1st Brigade" };
            unionFirstBrigade.Officer = new Officer() { Name = "Robert C. Schenck", OfficerRank = OfficerRank.BrigadierGeneral };
            unionFirstDivision.AddBrigade(unionFirstBrigade);



            Army aofP = new Army() { Name = "Army of the Potomac" };
            confederacy.AddArmy(aofP);
            aofP.Officer = new Officer() { Name = "P. G. T. Beauregard", OfficerRank = OfficerRank.BrigadierGeneral };

            Corps confedFirstCorps = new Corps() { Name = "1st Corps" };
            aofP.AddCorps(confedFirstCorps);

            Army aofS = new Army() { Name = "Army of the Shenandoah" };
            confederacy.AddArmy(aofS);
            aofS.Officer = new Officer() { Name = "Joseph E. Johnston", OfficerRank = OfficerRank.BrigadierGeneral };

            Corps confedSecondCorps = new Corps() { Name = "2nd Corps" };
            aofS.AddCorps(confedSecondCorps);

            // save the file
            new PersistenceManager().SaveScenario(firstBullRun, "firstbullrun.scn");

        }
    }
}

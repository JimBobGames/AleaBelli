﻿using AleaBelli.Core.Data;
using AleaBelli.Core.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestScenario
    {
        [TestMethod]
        public void TestFirstBullRun()
        {
            // create the scenario programatically
            Scenario firstBullRun = new Scenario() { Name = "First Bull Run", ShortName = "First Bull Run", Description = "",
                Date = new DateTime(1861, 07, 21, 08, 00, 0) };

            Army aofNEV = new Army() { Name = "Army of Northeastern Virginia" };
            firstBullRun.AddArmy(aofNEV);
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
            firstBullRun.AddArmy(aofP);
            aofP.Officer = new Officer() { Name = "P. G. T. Beauregard", OfficerRank = OfficerRank.BrigadierGeneral };

            Corps confedFirstCorps = new Corps() { Name = "1st Corps" };
            aofP.AddCorps(confedFirstCorps);

            Army aofS = new Army() { Name = "Army of the Shenandoah" };
            firstBullRun.AddArmy(aofS);
            aofS.Officer = new Officer() { Name = "Joseph E. Johnston", OfficerRank = OfficerRank.BrigadierGeneral };

            Corps confedSecondCorps = new Corps() { Name = "2nd Corps" };
            aofS.AddCorps(confedSecondCorps);

            // save the file
            new PersistenceManager().SaveScenario(firstBullRun, "firstbullrun.scn");

        }
    }
}
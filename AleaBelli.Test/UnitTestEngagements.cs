using AleaBelli.Core.Data;
using AleaBelli.Core.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static AleaBelli.Core.Data.Engagement;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestEngagements
    {
        [TestMethod]
        public void TestEngagement1()
        {
            // get some weapons
            Weapon smoothboreFlintlock = Reference.GetInstance().Weapons[2];

            Regiment attacker = new Regiment()
            {
                Name = "The Duke of Marlborough's Regiment of Foot",
                Men = 524,
                WeaponId = smoothboreFlintlock.WeaponId,
                RegimentFormation = RegimentFormation.Line3,
            };

            Regiment defender = new Regiment()
            {
                Name = "1st Battalion, Regiment de Navarre",
                Men = 500,
                WeaponId = smoothboreFlintlock.WeaponId,
                RegimentFormation = RegimentFormation.Line3,
            };

            Engagement e = new Engagement()
            {
                Attacker = attacker,
                Distance = 1000,
                Terrain = Battle.Terrain.Clear,
                Weather = Battle.Weather.Clear,
            };
            e.Defenders.Add(defender);

            CombatEngine combatEngine = new CombatEngine();
            EngagementResult result = combatEngine.ResolveEngagement(e);
        }
    }
}

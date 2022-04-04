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
                DoctrineId = Reference.PointBlankChargeDoctrineId,
            };

            Regiment defender = new Regiment()
            {
                Name = "1st Battalion, Regiment de Navarre",
                Men = 500,
                WeaponId = smoothboreFlintlock.WeaponId,
                RegimentFormation = RegimentFormation.Line3,
                DoctrineId = Reference.PointBlankChargeDoctrineId,
            };

            Engagement e = new Engagement()
            {
                Attacker = attacker,
                Duration = 0,
                Distance = 1000,
                Terrain = Battle.Terrain.Clear,
                Weather = Battle.Weather.Clear,
                FirstVolleyFired = false,
            };
            e.Defenders.Add(defender);

            CombatEngine combatEngine = new CombatEngine();
            EngagementResult result = combatEngine.ResolveEngagement(e);
        }

        [TestMethod]
        public void TestEngagement2()
        {
            // get some weapons
            Weapon enfield1853 = Reference.GetInstance().Weapons[4];
            Weapon smoothboreFlintlock = Reference.GetInstance().Weapons[2];

            Regiment attacker = new Regiment()
            {
                Name = "The Duke of Marlborough's Regiment of Foot",
                Men = 524,
                WeaponId = enfield1853.WeaponId,
                RegimentFormation = RegimentFormation.Line3,
                DoctrineId = Reference.EffectiveExchangeDoctrineId,
            };

            Regiment defender = new Regiment()
            {
                Name = "1st Battalion, Regiment de Navarre",
                Men = 500,
                WeaponId = smoothboreFlintlock.WeaponId,
                RegimentFormation = RegimentFormation.Line3,
                DoctrineId = Reference.EffectiveExchangeDoctrineId,
            };

            Engagement e = new Engagement()
            {
                Attacker = attacker,
                Duration = 0,
                Distance = 1000,
                Terrain = Battle.Terrain.Clear,
                Weather = Battle.Weather.Clear,
                FirstVolleyFired = false,

            };
            e.Defenders.Add(defender);

            CombatEngine combatEngine = new CombatEngine();
            EngagementResult result = combatEngine.ResolveEngagement(e);
        }
    }
}


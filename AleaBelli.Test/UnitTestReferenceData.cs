using AleaBelli.Core.Data;
using AleaBelli.Core.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestReferenceData
    {
        [TestMethod]
        public void TestWeapons()
        {
            PersistenceManager pm = new PersistenceManager();
            
            // test writing data
            Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();
            Weapon brownBess = new Weapon()
            {
                WeaponId = 1,
                Name = "Brown Bess",
                Year = 1722,
                FireRate = 3,
                EffectiveRange = 300,
                Accuracy = 25,
            };
            weapons[brownBess.WeaponId] = brownBess;

            Weapon smoothboreFlintlock = new Weapon()
            {
                WeaponId = 2,
                Name = "Smoothbore Flintlock",
                Year = 1700,
                FireRate = 3,
                EffectiveRange = 300,
                Accuracy = 25,
            };
            weapons[smoothboreFlintlock.WeaponId] = smoothboreFlintlock;

            Weapon chassepot = new Weapon()
            {
                WeaponId = 3,
                Name = "Chassepot",
                Year = 1866,
                FireRate = 3,
                EffectiveRange = 300,
                Accuracy = 25,
            };
            weapons[chassepot.WeaponId] = chassepot;

            Weapon enfieldMusket = new Weapon()
            {
                WeaponId = 4,
                Name = "Pattern 1853 Enfield",
                Year = 1853,
                FireRate = 3,
                EffectiveRange = 300,
                Accuracy = 25,
            };
            weapons[enfieldMusket.WeaponId] = enfieldMusket;

            pm.SaveObject(weapons, "weapons.txt");
            

            Reference refData = Reference.GetInstance();
            Assert.IsNotNull(refData);  
            Assert.IsNotNull(refData.Weapons);
            Assert.IsTrue(refData.Weapons.Count > 1);

        }
    }
}

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
            };
            weapons[brownBess.WeaponId] = brownBess;

            Weapon smoothboreFlintlock = new Weapon()
            {
                WeaponId = 2,
                Name = "Smoothbore Flintlock",
            };
            weapons[smoothboreFlintlock.WeaponId] = smoothboreFlintlock;

            pm.SaveObject(weapons, "weapons.txt");
            

            Reference refData = Reference.GetInstance();
            Assert.IsNotNull(refData);  
            Assert.IsNotNull(refData.Weapons);
            Assert.IsTrue(refData.Weapons.Count > 1);

        }
    }
}

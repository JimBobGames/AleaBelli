using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AleaBelli.Core.Network;
using AleaBelli.Core.Persistence;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestReferenceData
    {
        private SinglePlayerAleaBelliGame game = new SinglePlayerAleaBelliGame();

        [TestMethod]
        public void TestReferenceData()
        {
        }

        [TestMethod]
        public void TestLoadNations()
        {
            ReferenceDataPersistence.LoadNations(game);
            Assert.IsTrue(game.AllNations.Count > 0, "No nations");

        }
    }
}

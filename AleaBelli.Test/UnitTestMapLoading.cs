using System;
using System.Collections.Generic;
using AleaBelli.Core.Hex;
using AleaBelli.Core.Network;
using AleaBelli.Core.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiledCS;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestMapLoading
    {
        private PersistenceManager persistenceManager = new PersistenceManager();

        [TestMethod]
        public void TestLoadMap()
        {
            TiledMap map = persistenceManager.LoadTiledMap(@"Maps\testmap.tmx");
        }

    }
}
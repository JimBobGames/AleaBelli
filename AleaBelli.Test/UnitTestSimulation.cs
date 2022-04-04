using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestSimulation
    {
        [TestMethod]
        public void TestSimulation()
        {
            using (StreamWriter file = File.CreateText(@"..\..\..\AleaBelli\Docs\simulation.html"))
            {
                file.WriteLine("<html>");
                file.WriteLine("<head>");
                file.WriteLine("<title>Alea Belli Simulation</title>");
                   file.WriteLine("</head>");
                file.WriteLine("<body>");

                WriteChart(file);


                file.WriteLine("</body>");
                file.WriteLine("</html>");
            }
        }

        private void WriteChart(StreamWriter file)
        {

            file.WriteLine("<div>");
            file.WriteLine("<canvas id = \"myChart\" ></canvas>");

            file.WriteLine("</div>");
        }
    }
}

//https://cdnjs.com/libraries/Chart.js
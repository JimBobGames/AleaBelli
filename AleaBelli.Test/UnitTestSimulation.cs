using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                file.WriteLine("<script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.1/chart.js'></script>");
                file.WriteLine("</head>");
                file.WriteLine("<body>");

                WriteChart(file, "Chart1", 
                    new int[] { 500, 200, 100, 50, 0, 0}, 
                    "Smoothbore Musket", 
                    new string[] { "0 Yards", "50 Yards", "100 Yards", "150 Yards", "200 Yards", "250 Yards", "300 Yards" });


                file.WriteLine("</body>");
                file.WriteLine("</html>");
            }
        }

        private void WriteChart(StreamWriter file, string chartname, int[] values, string dataset, string[] labels)
        {

            file.WriteLine("<div>");
            file.WriteLine("<canvas id = \"" + chartname + "\" ></canvas>");
            file.WriteLine("<script>");
            StringBuilder sb = new StringBuilder();
            sb.Append(" const labels = [");
            foreach (string l in labels)
                sb.Append(" '" + l + "', ");
            sb.Append(" ]; ");
            file.WriteLine(sb.ToString());


            file.WriteLine("const data = {");
            file.WriteLine("labels: labels,");
            file.WriteLine(" datasets:");
            file.WriteLine("        [{");
            file.WriteLine("       label: '" + dataset + "',");
            file.WriteLine("  backgroundColor: 'rgb(255, 99, 132)',");
            file.WriteLine(" borderColor: 'rgb(255, 99, 132)',");
            //file.WriteLine(" data: [0, 10, 5, 2, 20, 30, 45],");

            sb = new StringBuilder();
            //file.WriteLine(" data: [0, 10, 5, 2, 20, 30, 45],");
            sb.Append(" data: [");
            foreach (int v in values)
                sb.Append(v + ",");
            sb.Append("],");
            file.WriteLine(sb.ToString());


            file.WriteLine(" }]");
            file.WriteLine(" };");

            file.WriteLine("const config = {");
            file.WriteLine("type: 'line',");
            file.WriteLine("data: data,");
            file.WriteLine("options: { }");
            file.WriteLine("};");

            file.WriteLine("</script>");


            file.WriteLine("<script>");
            file.WriteLine("const myChart = new Chart(");
            file.WriteLine("document.getElementById('" + chartname + "'),");
            file.WriteLine(" config");
            file.WriteLine(");");

            file.WriteLine("</script>");
            file.WriteLine("</div>");
        }
    }
}

//https://cdnjs.com/libraries/Chart.js
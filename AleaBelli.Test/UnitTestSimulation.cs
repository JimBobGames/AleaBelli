using AleaBelli.Core.Data;
using AleaBelli.Core.Engine;
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

                //Weapon w = Reference.GetInstance().Weapons[4]; // enfield
                Weapon w = Reference.GetInstance().Weapons[2]; // smooth bore
                int[] values = GenerateWeaponHits(w);

                WriteChart(file, "Chart1", 
                    values,
                    //new int[] { 500, 200, 100, 50, 0, 0}, 
                    w.Name, 
                    new string[] { "0 Yards", "50 Yards", "100 Yards", "150 Yards", "200 Yards", "250 Yards", "300 Yards" });
                /*
                WriteChart(file, "Chart2",
                    new int[] { 500, 200, 100, 50, 0, 0 },
                    "Brown Bess",
                    new string[] { "0 Yards", "50 Yards", "100 Yards", "150 Yards", "200 Yards", "250 Yards", "300 Yards" });
                */
                file.WriteLine("</body>");
                file.WriteLine("</html>");
            }
        }

        private int[] GenerateWeaponHits(Weapon w)
        {
            CombatEngine e = new CombatEngine();
            int[] values = new int[7];
            values[0] = e.DoFireWeapon(w, 0, 1000);
            values[1] = e.DoFireWeapon(w, 50, 1000);
            values[2] = e.DoFireWeapon(w, 100, 1000);
            values[3] = e.DoFireWeapon(w, 150, 1000);
            values[4] = e.DoFireWeapon(w, 200, 1000);
            values[5] = e.DoFireWeapon(w, 250, 1000);
            values[6] = e.DoFireWeapon(w, 300, 1000);

            return values;
        }

        private void WriteChart(StreamWriter file, string chartname, int[] values, string dataset, string[] labels)
        {

            file.WriteLine("<div style=\"position: relative; height: 40vh; width: 80vw\">");
            file.WriteLine("<canvas id =\"" + chartname + "\" ></canvas>");
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
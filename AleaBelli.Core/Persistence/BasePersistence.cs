using AleaBelli.Core.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Persistence
{
    public class BasePersistence
    {
        public static string baseFilePath = "";
        public static bool defaultUseFile = false;

        public static List<string> ReadTextResource(IAleaBelliGame g, string filename)
        {
            List<string> lines = new List<string>();

            if (defaultUseFile)
            {
                return ReadTextFileResource(g, baseFilePath + filename);
            }

            var assembly = g.GetType().Assembly;
            var names = g.GetType().Assembly.GetManifestResourceNames();
            foreach (string name in names)
            {
                if (name.EndsWith(filename))
                {
                    // var resource = assembly.GetManifestResourceStream(name);
                    using (Stream stream = assembly.GetManifestResourceStream(name))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            while (reader.Peek() >= 0)
                            {
                                string line = reader.ReadLine();
                                if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
                                {
                                    lines.Add(line);
                                }
                            }
                        }
                    }
                }
            }

            return lines;
        }

        public static List<string> ReadTextFileResource(IAleaBelliGame g, string filename)
        {
            List<string> lines = new List<string>();
            // Open the text file using a stream reader.
            using (StreamReader reader = new StreamReader(filename))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
                    {
                        lines.Add(line);
                    }
                }
            }
            return lines;
        }

        public static string ReadStringResource(IAleaBelliGame g, string filename)
        {
            List<string> list = ReadTextResource(g, filename);
            StringBuilder sb = new StringBuilder();
            foreach (string s in list)
            {
                sb.AppendLine(s);
            }

            return sb.ToString();
        }
        /*
        public static List<string> ReadTextResource(AleaBelliGame g, string filename)
        {
            List<string> lines = new List<string>();
            var assembly = g.GetType().GetTypeInfo().Assembly;
            var names = g.GetType().GetTypeInfo()
             .Assembly
             .GetManifestResourceNames();
            foreach (string name in names)
            {
                if (name.EndsWith(filename))
                {
                    using (Stream stream = assembly.GetManifestResourceStream(name))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            while (reader.Peek() >= 0)
                            {
                                string line = reader.ReadLine();
                                if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
                                {
                                    lines.Add(line);
                                }
                            }
                        }
                    }
                }
            }

            return lines;
        }
        */
    }
}

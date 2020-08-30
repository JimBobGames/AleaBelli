﻿using AleaBelli.Core.Data;
using AleaBelli.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.UI
{
    /// <summary>
    /// Class to handle UI updates
    /// </summary>
    public class Controller
    {
        public AleaBelliGame Game { get; set; }

        public void OnClickRegiment(Regiment r)
        {
            if (r != null)
            {
                // something has been clicked
                Console.WriteLine(r.Name);
            }

        }
    }
}
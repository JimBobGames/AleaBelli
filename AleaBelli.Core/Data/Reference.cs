using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AleaBelli.Core.Data
{
    public sealed class Reference
    {
        private Reference() { }

        private Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();    

        private static Reference _instance;

        public static Reference GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Reference();
                _instance.Load();
            }
            return _instance;
        }

        private void Load()
        {
            LoadWeapons();
        }

        public Dictionary<int, Weapon> Weapons
            { get { return weapons; } } 


        private void LoadWeapons()
        {
            weapons = JsonConvert.DeserializeObject<Dictionary<int, Weapon>>(File.ReadAllText("weapons.txt"));
        }

    }
}

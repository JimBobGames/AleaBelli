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

        private Dictionary<int, Doctrine> doctrines = new Dictionary<int, Doctrine>();

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
            LoadDoctrines();
            LoadWeapons();
        }

        private void LoadDoctrines()
        {
            AdvanceVolleyCharge avcDoctrine = new AdvanceVolleyCharge()
            {
                DoctrineId = 1,
                Name = "Advance, Volley, Charge",
            };
            doctrines[avcDoctrine.DoctrineId] = avcDoctrine;

            AdvanceExchangeCharge aecDoctrine = new AdvanceExchangeCharge()
            {
                DoctrineId = 2,
                Name = "Advance, Exchange, Charge",
            };
            doctrines[aecDoctrine.DoctrineId] = aecDoctrine;
        }

        public Dictionary<int, Weapon> Weapons
            { get { return weapons; } } 


        private void LoadWeapons()
        {
            weapons = JsonConvert.DeserializeObject<Dictionary<int, Weapon>>(File.ReadAllText("weapons.txt"));
        }

    }
}

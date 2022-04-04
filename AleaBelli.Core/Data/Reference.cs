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

        public const int PointBlankChargeDoctrineId = 1;
        public const int EffectiveChargeDoctrineId = 2;
        public const int EffectiveExchangeDoctrineId = 3;

        private void LoadDoctrines()
        {
            AdvancePointBlankVolleyCharge avpbDoctrine = new AdvancePointBlankVolleyCharge()
            {
                DoctrineId = PointBlankChargeDoctrineId,
                Name = "Advance Point Blank, Volley, Charge",
            };
            doctrines[avpbDoctrine.DoctrineId] = avpbDoctrine;

            AdvanceEffectiveVolleyCharge aveffDoctrine = new AdvanceEffectiveVolleyCharge()
            {
                DoctrineId = EffectiveChargeDoctrineId,
                Name = "Advance Effective, Volley, Charge",
            };
            doctrines[aveffDoctrine.DoctrineId] = aveffDoctrine;

            AdvanceExchangeCharge aecDoctrine = new AdvanceExchangeCharge()
            {
                DoctrineId = EffectiveExchangeDoctrineId,
                Name = "Advance, Exchange, Charge",
            };
            doctrines[aecDoctrine.DoctrineId] = aecDoctrine;
        }

        public Dictionary<int, Weapon> Weapons
            { get { return weapons; } }

        public Dictionary<int, Doctrine> Doctrines
        { get { return doctrines; } }

        private void LoadWeapons()
        {
            weapons = JsonConvert.DeserializeObject<Dictionary<int, Weapon>>(File.ReadAllText("weapons.txt"));
        }

    }
}

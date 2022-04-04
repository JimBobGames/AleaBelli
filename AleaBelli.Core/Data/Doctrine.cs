using AleaBelli.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public abstract class Doctrine : SimpleObject
    {
        public const int DoubleTime = 150; // charge
        public const int QuickTime = 100;  // trained troops on good terrain
        public const int OrdinaryTime = 50; // sustainable for long periods


        public int DoctrineId { get; set; }

        public virtual int GetMoveDistance(Engagement e, Regiment r)
        {
            return OrdinaryTime;
        }

        public abstract int GetDesiredRange(Regiment r);

        public int FireWeapon(Engagement e, Regiment r)
        {
            int targetRange = GetDesiredRange(r);
            if(e.Distance <= targetRange)
            {
                return DoFireWeapon(r, e.Defenders[0], e);
            }

            return 0;
        }

        public int DoFireWeapon(Regiment firer, Regiment target, Engagement e)
        {
            Weapon w = firer.GetWeapon();
            int accuracy = w.Accuracy;

            if(e.Distance <= 0)
            {
                return 0;
            }

            // adjust the accuracy for range
            int rangeSteps = e.Distance / 100;
            for (int i = 0;i < rangeSteps; i++)
            {
                accuracy = accuracy / 2;
            }


            int hits = (accuracy * firer.Men) / 100;

            // variation 10 %
           // int percent = Rand.RandD100();
            int percent = Rand.RandN(50) + 25; // troop quality ??

            hits = (hits * percent) / 100;

            return hits;
        }
    }



    /// <summary>
    /// Advance to effective range fire one volley charge
    /// </summary>
    public class AdvancePointBlankVolleyCharge : Doctrine
    {
        public override int GetMoveDistance(Engagement e, Regiment r)
        {
            // have we reached the distance for our volley
            Weapon w = e.Attacker.GetWeapon();

            // are we at 50 yards ???
            if (e.Distance <= 100)
            {
                if(e.FirstVolleyFired)
                {
                    return e.Distance;
                }
                return e.Distance - 50;
            }


            // check the terrain
            if(e.Terrain != Battle.Terrain.Clear)
            {
                return OrdinaryTime;
            }

            // how long can a unit sustain quick time ??? say 10 mminutes
            if (r.Fatigue <= 10)
            {
                // are we close enough ?
                if(e.Distance <= 1000)
                {
                    return QuickTime;
                }


            }


            return OrdinaryTime;
        }

        public override int GetDesiredRange(Regiment r)
        {
            return 50;
        }
    }

    public class AdvanceEffectiveVolleyCharge : Doctrine
    {
        public override int GetMoveDistance(Engagement e, Regiment r)
        {
            // have we reached the distance for our volley
            Weapon w = r.GetWeapon();
            int effectiveRange = w.EffectiveRange;

            // are we at effective range yards ???
            if (e.Distance <= effectiveRange)
            {
                return e.Distance - effectiveRange;
            }


            // check the terrain
            if (e.Terrain != Battle.Terrain.Clear)
            {
                return OrdinaryTime;
            }

            // how long can a unit sustain quick time ??? say 10 mminutes
            if (r.Fatigue <= 10)
            {
                // are we close enough ?
                if (e.Distance <= 1000)
                {
                    return QuickTime;
                }


            }


            return OrdinaryTime;
        }


        public override int GetDesiredRange(Regiment r)
        {
            return r.GetWeapon().EffectiveRange;
        }

    }

    /// <summary>
    /// Advance to effective range fire multiple volleys
    /// </summary>
    public class AdvanceExchangeCharge : Doctrine
    {
        public override int GetMoveDistance(Engagement e, Regiment r)
        {
            // have we reached the distance for our volley
            Weapon w = e.Attacker.GetWeapon();
            int effectiveRange = w.EffectiveRange;

            // are we at effective range yards ???
            if (e.Distance <= effectiveRange)
            {
                return e.Distance - effectiveRange;
            }


            // check the terrain
            if (e.Terrain != Battle.Terrain.Clear)
            {
                return OrdinaryTime;
            }

            // how long can a unit sustain quick time ??? say 10 mminutes
            if (r.Fatigue <= 10)
            {
                // are we close enough ?
                if (e.Distance <= 1000)
                {
                    return QuickTime;
                }


            }


            return OrdinaryTime;
        }


        public override int GetDesiredRange(Regiment r)
        {
            return r.GetWeapon().EffectiveRange;
        }

    }
}

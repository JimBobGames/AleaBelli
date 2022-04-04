﻿using AleaBelli.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AleaBelli.Core.Data.Engagement;

namespace AleaBelli.Core.Engine
{
    public class CombatEngine
    {
        public EngagementResult ResolveEngagement(Engagement e)
        {
            // for each 'round' is 1 minute
            EngagementResult result = EngagementResult.Engaged;

            Doctrine attackerDoctrine = Reference.GetInstance().Doctrines[e.Attacker.DoctrineId];
            Doctrine defenderDoctrine = Reference.GetInstance().Doctrines[e.Defenders[0].DoctrineId];

            while (result == EngagementResult.Engaged)
            {
                // increment the duration
                e.Duration++;

                // move the units
                int attackerMove = attackerDoctrine.GetMoveDistance(e, e.Attacker);
                e.Distance -= attackerMove;

                int attackerHits = attackerDoctrine.FireWeapon(e, e.Attacker);
                if(attackerHits > 0)
                {
                    e.FirstVolleyFired = true;
                }

                int defenderHits = defenderDoctrine.FireWeapon(e, e.Defenders[0]);


                Console.WriteLine("Turn " + e.Duration + ", Attacker Move " + attackerMove + ", Distance " + e.Distance);
                if (attackerHits > 0)
                {
                    Console.WriteLine(e.Attacker.Name + " at " + e.Distance + " " + e.Attacker.GetWeapon().Name + " " + attackerHits);
                }
                if (defenderHits > 0)
                {
                    Console.WriteLine(e.Defenders[0].Name + " at " + e.Distance + " " + e.Defenders[0].GetWeapon().Name + " " + defenderHits);
                }
                // decide if fire - howe many shots ?


                // morale test


                // check if theres a result
                if (e.Duration > 60)
                {
                    result = EngagementResult.Draw;
                }
                    
                if(e.Distance <= 0)
                {
                    // fight melee ???

                    result = EngagementResult.AttackerWin;
                }
            }
            Console.WriteLine("Result " + result);
            return result;
        }
    }
}

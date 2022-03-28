using AleaBelli.Core.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AleaBelli.Test
{
    [TestClass]
    public class UnitTestCombat
    {
        // https://rodwargaming.wordpress.com/miltary-historical-research/military-historical-research/napoleonic-infantry-march-rates/
        // https://www.britannica.com/topic/tactics/Tactics-from-Waterloo-to-the-Bulge

        [TestMethod]
        public void TestRegimentFiring()
        {
            Regiment r1 = new Regiment();

            Regiment r2 = new Regiment();

            // FireResult result = r1.Fire(r2, range, weather);
            // morale check ??

            // plot bell curve

            // model 10 minutes
            // regiment approaches stationary target - how fast in a minute
            // movement speed normal, quick charge ???
            // normal 75 paces ( 1 pace = 30 inches, 75 cm, 0.75 m
            // paces = 75 * 0.75 = 56 m eters per minute (normal pace)
            // how many shots do the defenders get ???

            // weapon musket + bayonet

            // defenders get 1 round before contact ???

            // volley fire, platoon fire, firing by ranks

            // blenheim - when to fire ???
            // attackets fire then charge, defenders hold fire
            // exchange fire
            // troop quality - guard unit - are they more accurate ? fire more frequently ? fire at wrong time
            // doctrine
            // 100 muskets, hit rate = 25 % - what does the dice roll do ??
            // does the dice roll adjust by a small percentage e.g. 20 - 30 % modified by quality???
            // fire doctrine - volley, 
            // drill
            // reverse slope

            //  battalion would approach the enemy, fire one or several volleys, and then charge the enemy with swords, pikes and (later) bayonets,
            //  a style they dubbed "Gå På" (which translates roughly as "Go at them").
            // 
            // platoon firing, which was perfected by the British during the 18th century: here, the battalion, lined up in three rows,
            // was divided into 24-30 platoons which would fire alternately, thus concentrating their fire. This required intensive training for the soldiers,
            // who had to operate their muskets in close ranks. After the command to make ready was given, the first rank knelt down, whilst the third rank stepped slightly to the right,
            // in order to level their muskets past the men in front of them. The French army had trouble adopting this method and relied for the most part of the 18th century on firing by ranks,
            // in which the first rank fired first, followed by the second, and then the third rank. This method was acknowledged by the French command at the time to be far less effective.
            // The Prussian army, reformed under the "Alte Dessauer", placed much emphasis on firepower. In order to make the men load and fire their muskets quicker, the iron ramrod was developed.
            // Voltaire once commented that the Prussian soldiers could load and fire their muskets seven times in a minute; this is a gross exaggeration, but it is an indication of the drill, which led to platoons firing devastating volleys with clockwork precision.
            // In all, a professional soldier was required to load and fire his musket three times in a minute.
        }
    }
}

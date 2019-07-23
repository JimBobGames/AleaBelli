using AleaBelli.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Network
{
    /// <summary>
    /// The interface which exposes game data to say the rendering system
    /// </summary>
    public interface IAleaBelliGame
    {
        List<Battalion> AllBattalions { get; }

        void AddBattalion(Battalion b);
        void AddNation(Nation n);
    }

    /// <summary>
    /// The simplest game type - no networking or threading
    /// </summary>
    public class SinglePlayerAleaBelliGame : AleaBelliGame
    {

    }

    /// <summary>
    /// The basic game data
    /// </summary>
    public abstract class AleaBelliGame : IAleaBelliGame
    {
        private Dictionary<int, Nation> nations = new Dictionary<int, Nation>();

        private Dictionary<int, Battalion> battalions = new Dictionary<int, Battalion>();

        internal Dictionary<int, Nation> Nations
        {
            get
            {
                return nations;
            }
        }

        internal Dictionary<int, Battalion> Battalions
        {
            get
            {
                return battalions;
            }
        }

        public void AddNation(Nation n)
        {
            nations[n.NationId] = n;
        }

        public void AddBattalion(Battalion r)
        {
            battalions[r.BattalionId] = r;
        }


        public List<Battalion> AllBattalions
        {
            get
            {
                return battalions.Values.ToList<Battalion>();
            }
        }

        public List<Nation> AllNations
        {
            get
            {
                return nations.Values.ToList<Nation>();
            }
        }
    }
}

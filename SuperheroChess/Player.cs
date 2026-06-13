using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelDcChess
{
    internal class Player
    {

        private string team = String.Empty;
        private bool isCPU = false;

        /// <summary>
        /// to create a player object
        /// </summary>
        /// <param name="team"></param>
        /// <param name="isCPU"></param>
        public Player(string team, bool isCPU)
        {
            this.team = team;
            this.isCPU = isCPU;
        }

        /// <summary>
        /// Gets or sets the player's team
        /// </summary>
        public string Team
        {
            get { return team; }
            set { team = value; }
        }

        /// <summary>
        /// Gets or sets whether the player is CPU 
        /// </summary>
        public bool IsCPU
        {
            get { return isCPU; }
            set { isCPU = value; }
        }
    }
}

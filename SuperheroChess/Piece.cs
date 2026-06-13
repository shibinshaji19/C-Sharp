using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelDcChess
{
    internal abstract class Piece
    {

        protected string team = String.Empty;
        protected string imagePath = String.Empty;
        protected string pieceName = String.Empty;

        /// <summary>
        /// Constructor to initialize piece
        /// </summary>
        public Piece(string team, string imagePath, string pieceName)
        {
            this.team = team;
            this.imagePath = imagePath;
            this.pieceName = pieceName;
        }

        /// <summary>
        /// Gets or sets the team of the piece
        /// </summary>
        public string Team
        {
            get { return team; }
            set { team = value; }
        }

        /// <summary>
        /// Gets or sets the image path of the piece
        /// </summary>
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        /// <summary>
        /// Gets or sets the piece name
        /// </summary>
        public string PieceName
        {
            get { return pieceName; }
            set { pieceName = value; }
        }

        /// <summary>
        /// Abstract method to validate movement.      
        /// </summary>
        public abstract bool IsValidMove(int currentRow, int currentColumn, int newRow, int newColumn, Board board);
    }
}

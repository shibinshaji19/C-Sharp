using System;

namespace MarvelDcChess
{
    /// <summary>
    /// King piece class.
    /// Inherits from the Piece base class.
    /// 
    /// Rule Citation:
    /// The king can move one square in any direction.
    /// Custom project rule:
    /// If the king moves to a square occupied by a friendly adjacent piece,
    /// the swap logic is handled in Manager.cs.
    /// </summary>
    internal class King : Piece
    {
        /// <summary>
        /// Constructor for the King piece.
        /// </summary>
        /// <param name="team">Team name (Marvel or DC).</param>
        /// <param name="imagePath">Image path of the piece.</param>
        public King(string team, string imagePath)
            : base(team, imagePath, "King")
        {
        }

        /// <summary>
        /// Checks whether the king move is valid.
        /// 
        /// Standard king movement:
        /// - one square in any direction
        /// 
        /// Custom project rule:
        /// - if the destination has a friendly adjacent piece,
        ///   the king may swap places with that piece
        /// - the actual swapping is handled in Manager.cs
        /// </summary>
        /// <param name="currentRow">Current row of the king.</param>
        /// <param name="currentColumn">Current column of the king.</param>
        /// <param name="newRow">Destination row.</param>
        /// <param name="newColumn">Destination column.</param>
        /// <param name="board">Board object.</param>
        /// <returns>True if the move is valid, otherwise false.</returns>
        public override bool IsValidMove(int currentRow, int currentColumn, int newRow, int newColumn, Board board)
        {
            int rowDiff = Math.Abs(newRow - currentRow);
            int colDiff = Math.Abs(newColumn - currentColumn);

            // The king must move to a different square.
            if (rowDiff == 0 && colDiff == 0)
            {
                return false;
            }

            // The king can move only one square in any direction.
            // This also supports the custom swap rule,
            // because swapping is only allowed with an adjacent friendly piece.
            if (rowDiff <= 1 && colDiff <= 1)
            {
                return true;
            }

            return false;
        }
    }
}
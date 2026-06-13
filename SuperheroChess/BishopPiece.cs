using System;

namespace MarvelDcChess
{
    /// <summary>
    /// Bishop piece class.
    /// Inherits from the Piece base class.
    /// 
    /// Rule Citation:
    /// Standard chess rule - The bishop moves diagonally
    /// and cannot jump over pieces.
    /// </summary>
    internal class Bishop : Piece
    {
        /// <summary>
        /// Constructor for the Bishop piece.
        /// </summary>
        /// <param name="team">Team name (Marvel or DC).</param>
        /// <param name="imagePath">Image path of the piece.</param>
        public Bishop(string team, string imagePath)
            : base(team, imagePath, "Bishop")
        {
        }

        /// <summary>
        /// Checks whether the bishop move is valid.
        /// 
        /// Bishop movement:
        /// - diagonal only
        /// - path must be clear
        /// </summary>
        /// <param name="currentRow">Current row of the bishop.</param>
        /// <param name="currentColumn">Current column of the bishop.</param>
        /// <param name="newRow">Destination row.</param>
        /// <param name="newColumn">Destination column.</param>
        /// <param name="board">Board object used to check path.</param>
        /// <returns>True if the move is valid, otherwise false.</returns>
        public override bool IsValidMove(int currentRow, int currentColumn, int newRow, int newColumn, Board board)
        {
            int rowDiff = Math.Abs(newRow - currentRow);
            int colDiff = Math.Abs(newColumn - currentColumn);

            // A bishop must move diagonally.
            // That means the row difference and column difference must be equal.
            if (rowDiff != colDiff || rowDiff == 0)
            {
                return false;
            }

            int rowStep;
            int colStep;

            // Decide row direction
            if (newRow > currentRow)
            {
                rowStep = 1;
            }
            else
            {
                rowStep = -1;
            }

            // Decide column direction
            if (newColumn > currentColumn)
            {
                colStep = 1;
            }
            else
            {
                colStep = -1;
            }

            int row = currentRow + rowStep;
            int col = currentColumn + colStep;

            // Check every square in the bishop's path.
            // If any square is occupied, the move is invalid.
            while (row != newRow && col != newColumn)
            {
                if (board.GetPiece(row, col) != null)
                {
                    return false;
                }

                row += rowStep;
                col += colStep;
            }

            return true;
        }
    }
}
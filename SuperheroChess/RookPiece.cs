using System;

namespace MarvelDcChess
{
    /// <summary>
    /// Rook piece class.
    /// Inherits from the Piece base class.
    /// 
    /// Rule Citation:
    /// Standard rook move = horizontal or vertical with a clear path.
    /// Custom project rule = the rook may jump over the first piece in its path,
    /// but after jumping it can move only one more square.
    /// </summary>
    internal class Rook : Piece
    {
        /// <summary>
        /// Constructor for the Rook piece.
        /// </summary>
        /// <param name="team">Team name (Marvel or DC).</param>
        /// <param name="imagePath">Image path of the piece.</param>
        public Rook(string team, string imagePath)
            : base(team, imagePath, "Rook")
        {
        }

        /// <summary>
        /// Checks whether the rook move is valid.
        /// 
        /// Standard move:
        /// - horizontal or vertical
        /// - path must be clear
        /// 
        /// Custom move:
        /// - rook may jump over the first piece in the path
        /// - after jumping, it may land only on the very next square
        /// </summary>
        /// <param name="currentRow">Current row of the rook.</param>
        /// <param name="currentColumn">Current column of the rook.</param>
        /// <param name="newRow">Destination row.</param>
        /// <param name="newColumn">Destination column.</param>
        /// <param name="board">Board object used to check path.</param>
        /// <returns>True if move is valid, otherwise false.</returns>
        public override bool IsValidMove(int currentRow, int currentColumn, int newRow, int newColumn, Board board)
        {
            // Rook must move in a straight line only.
            if (currentRow != newRow && currentColumn != newColumn)
            {
                return false;
            }

            // Same square is not a valid move.
            if (currentRow == newRow && currentColumn == newColumn)
            {
                return false;
            }

            int rowStep = 0;
            int colStep = 0;

            // Find row direction
            if (newRow > currentRow)
            {
                rowStep = 1;
            }
            else if (newRow < currentRow)
            {
                rowStep = -1;
            }

            // Find column direction
            if (newColumn > currentColumn)
            {
                colStep = 1;
            }
            else if (newColumn < currentColumn)
            {
                colStep = -1;
            }

            int row = currentRow + rowStep;
            int col = currentColumn + colStep;

            int piecesInPath = 0;
            int firstPieceRow = -1;
            int firstPieceCol = -1;

            // Check all squares between current and destination
            while (row != newRow || col != newColumn)
            {
                if (board.GetPiece(row, col) != null)
                {
                    piecesInPath++;

                    // Save location of the first piece found
                    if (piecesInPath == 1)
                    {
                        firstPieceRow = row;
                        firstPieceCol = col;
                    }
                }

                row += rowStep;
                col += colStep;
            }

            // Normal rook move: path must be completely clear
            if (piecesInPath == 0)
            {
                return true;
            }

            // Custom rook jump:
            // there must be exactly one piece in the path,
            // and the rook must land on the square immediately after that piece
            if (piecesInPath == 1)
            {
                int requiredRow = firstPieceRow + rowStep;
                int requiredCol = firstPieceCol + colStep;

                if (newRow == requiredRow && newColumn == requiredCol)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
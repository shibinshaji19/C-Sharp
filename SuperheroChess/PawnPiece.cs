// File Name: PawnPiece.cs
// Author: Nathan Bricknell
// Date: April 8, 2026
// Purpose: To create a modified chess piece that is similar
//          to the standard pawn piece, with an additional
//          diagonal move that the player can make without
//          the need to eliminate another piece.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelDcChess
{
    internal class Pawn : Piece
    {
        public Pawn(string team, string imagePath)
            : base(team, imagePath, "Pawn")
        {
        }

        // CHeck to see if it is a legal move within the game
        public override bool IsValidMove(int currentRow, int currentColumn, int newRow, int newColumn, Board board)
        {
            int direction = (team == "Marvel") ? -1 : 1;

            int rowDiff = newRow - currentRow;
            int colDiff = Math.Abs(newColumn - currentColumn);

            string target = board.GetPiece(newRow, newColumn);

            // 1-step move
            if (colDiff == 0 && rowDiff == direction)
            {
                if (target == null)
                    return true;
            }

            // 2-step move
            if (colDiff == 0 && rowDiff == 2 * direction)
            {
                if ((team == "Marvel" && currentRow == 6) ||
                    (team == "DC" && currentRow == 1))
                {
                    int middleRow = currentRow + direction;

                    if (board.GetPiece(middleRow, currentColumn) == null &&
                        target == null)
                    {
                        return true;
                    }
                }
            }

            // Diagonal move
            if (colDiff == 1 && rowDiff == direction)
            {
                return true;
            }

            return false;
        }
    }
}
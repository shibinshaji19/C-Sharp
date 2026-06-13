// File Name: KnightPiece.cs
// Author: Nathan Bricknell
// Date: April 9, 2026
// Purpose: To create a modified chess piece that is similar
//          to the standard knight piece, with an additional
//          square move that the player can optionally make.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelDcChess
{
    internal class Knight : Piece
    {
        // Path to the image file
        public Knight(string team, string imagePath)
            : base(team, imagePath, "Knight")
        {
        }

        public override bool IsValidMove(int currentRow, int currentColumn, int newRow, int newColumn, Board board)
        {
            // Claculating how far the piece is moving
            int rowDiff = Math.Abs(newRow - currentRow);
            int colDiff = Math.Abs(newColumn - currentColumn);

            // Normal move
            if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
            {
                return true;
            }

            // 2,2 move
            if (rowDiff == 2 && colDiff == 2)
            {
                return true;
            }

            return false;
        }
    }
}

// File Name: QueenPiece.cs
// Author: Nathan Bricknell
// Date: April 8, 2026
// Purpose: To create a modified chess piece that is similar
//          to the standard queen piece, with an additional
//          knight move the player can also make.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelDcChess
{
    internal class Queen : Piece
    {
        public Queen(string team, string imagePath)
            : base(team, imagePath, "Queen")
        {
        }

        public override bool IsValidMove(int currentRow, int currentColumn, int newRow, int newColumn, Board board)
        {
            // Checking movement distance
            int rowDiff = Math.Abs(newRow - currentRow);
            int colDiff = Math.Abs(newColumn - currentColumn);

            // Knight move
            if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
            {
                return true;
            }

            // Horizontal move
            if (currentRow == newRow)
            {
                return IsPathClear(currentRow, currentColumn, newRow, newColumn, board);
            }

            // Vertical move
            if (currentColumn == newColumn)
            {
                return IsPathClear(currentRow, currentColumn, newRow, newColumn, board);
            }

            // Diagonal move
            if (rowDiff == colDiff)
            {
                return IsPathClear(currentRow, currentColumn, newRow, newColumn, board);
            }

            return false;
        }

        // Check if the path is clear from team pieces
        private bool IsPathClear(int currentRow, int currentColumn, int newRow, int newColumn, Board board)
        {
            // Which direction to move
            int rowStep = Math.Sign(newRow - currentRow);
            int colStep = Math.Sign(newColumn - currentColumn);

            int row = currentRow + rowStep;
            int col = currentColumn + colStep;
            // loop to check each square if blocked from team piece
            while (row != newRow || col != newColumn)
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

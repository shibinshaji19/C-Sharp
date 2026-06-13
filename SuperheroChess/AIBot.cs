// File Name: AIBot.cs
// Author: Nathan Bricknell
// Date: April 18, 2026
// Purpose: To make an AI player that will compete against
//          the user

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelDcChess
{
    internal class AIBot
    {
        private Manager manager;
        private Random rand = new Random();

        public AIBot(Manager manager)
        {
            this.manager = manager;
        }

        public void MakeMove()
        {
            // Get the board and board state from manager
            var board = manager.GetBoard();
            var state = board.GetBoardState();

            // loop through board rows
            for (int i = 0; i < 8; i++)
            {
                // Loop through board columns
                for (int j = 0; j < 8; j++)
                {
                    string piece = state[i, j];
                    // If square is empty, skip
                    if (piece == null)
                        continue;

                    // only move DC pieces (AI)
                    if (!piece.Contains("batman") &&
                        !piece.Contains("night wing") &&
                        !piece.Contains("raven") &&
                        !piece.Contains("wonder woman") &&
                        !piece.Contains("Prof K") &&
                        !piece.Contains("GCPD"))
                    {
                        continue;
                    }

                    // Convert into a piece object
                    Piece p = manager.CreatePieceObject(piece);

                    if (p == null)
                        continue;

                    // try random moves
                    for (int attempt = 0; attempt < 20; attempt++)
                    {
                        int r = rand.Next(8);
                        int c = rand.Next(8);

                        // Check if the move is valid
                        if (p.IsValidMove(i, j, r, c, board))
                        {
                            if (manager.MoveIfValid(i, j, r, c))
                                return;
                        }
                    }
                }
            }
        }
    }
}
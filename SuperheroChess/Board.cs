using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelDcChess
{

    class Board
    {
        // 2d array to store board state
        private string[,] boardState = new string[8, 8];
        public Board()
        {
            InitializeBoard();
        }
        /// <summary>
        /// Returns the entire board state
        /// </summary>
        /// <returns></returns>
        public string[,] GetBoardState()
        {
            return boardState;
        }
        /// <summary>
        /// Returns piece image path at location
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetPiece(int row, int column)
        {
            return boardState[row, column];
        }
        /// <summary>
        /// Set piece at a location using image path
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="piece"></param>
        public void SetPiece(int row, int column, string pieceLocation)
        {
            boardState[row, column] = pieceLocation;
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardState[i, j] = null;
                }
            }
            // Setting images to array as strings
            boardState[0, 0] = "/Images/batman.png";
            boardState[0, 1] = "/Images/night wing.png";
            boardState[0, 2] = "/Images/Raven.png";
            boardState[0, 3] = "/Images/wonder woman.png";
            boardState[0, 4] = "/Images/Prof K.png";
            boardState[0, 5] = "/Images/Raven.png";
            boardState[0, 6] = "/Images/night wing.png";
            boardState[0, 7] = "/Images/batman.png";
            boardState[1, 0] = "/Images/GCPD.png";
            boardState[1, 1] = "/Images/GCPD.png";
            boardState[1, 2] = "/Images/GCPD.png";
            boardState[1, 3] = "/Images/GCPD.png";
            boardState[1, 4] = "/Images/GCPD.png";
            boardState[1, 5] = "/Images/GCPD.png";
            boardState[1, 6] = "/Images/GCPD.png";
            boardState[1, 7] = "/Images/GCPD.png";
            boardState[7, 0] = "/Images/iron man.png";
            boardState[7, 1] = "/Images/spider man.png";
            boardState[7, 2] = "/Images/doctor strange.png";
            boardState[7, 3] = "/Images/scarlet witch.png";
            boardState[7, 4] = "/Images/Prof X.png";
            boardState[7, 5] = "/Images/doctor strange.png";
            boardState[7, 6] = "/Images/spider man.png";
            boardState[7, 7] = "/Images/iron man.png";
            boardState[6, 0] = "/Images/sheed.png";
            boardState[6, 1] = "/Images/sheed.png";
            boardState[6, 2] = "/Images/sheed.png";
            boardState[6, 3] = "/Images/sheed.png";
            boardState[6, 4] = "/Images/sheed.png";
            boardState[6, 5] = "/Images/sheed.png";
            boardState[6, 6] = "/Images/sheed.png";
            boardState[6, 7] = "/Images/sheed.png";
        }
        /// <summary>
        /// Moves a piece from one position to other
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="newRow"></param>
        /// <param name="newColumn"></param>
        /// <returns></returns>
        public bool MovePiece(int row, int column, int newRow, int newColumn)
        {
            // Check if there is a piece exist at the position
            if (boardState[row, column] == null)
            {
                return false;
            }
            // Move piece to new position
            boardState[newRow, newColumn] = boardState[row, column];
            // Clear old position
            boardState[row, column] = null;
            return true;
        }

        public string[][] GetBoardStateJagged()
        {
            string[][] result = new string[8][];

            for (int i = 0; i < 8; i++)
            {
                result[i] = new string[8];
                for (int j = 0; j < 8; j++)
                {
                    result[i][j] = boardState[i, j];
                }
            }

            return result;
        }

        public void SetBoardStateFromJagged(string[][] data)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardState[i, j] = data[i][j];
                }
            }
        }
    }
}

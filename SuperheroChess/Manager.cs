using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MarvelDcChess
{
    internal class Manager
    {

        // To store all piece positions
        private Board gameBoard;
        /// <summary>
        /// get or set whose turn it is in the game
        /// </summary>
        public bool IsMarvelTurn { get; set; }
        /// <summary>
        /// shows if the game has ended
        /// </summary>
        private bool IsGameOver { get; set; }
        /// <summary>
        /// Creates a new board and starts the game
        /// </summary>
        public Manager()
        {
            gameBoard = new Board();
            StartGame();
        }
        /// <summary>
        /// start new game by resetting turn and game status
        /// </summary>
        public void StartGame()
        {
            IsMarvelTurn = true;
            IsGameOver = false;
        }
        /// <summary>
        /// Returns current board object
        /// </summary>
        /// <returns></returns>

        public Board GetBoard()
        {
            return gameBoard;
        }
        /// <summary>
        /// To check if its marvels turn
        /// </summary>
        /// <returns></returns>
        public bool GetCurrentTurn()
        {
            return IsMarvelTurn;
        }
        /// <summary>
        /// change turn from marvel to dc
        /// </summary>
        public void ChangeTurn()
        {
            IsMarvelTurn = !IsMarvelTurn;
        }
        /// <summary>
        /// check logic
        /// </summary>
        /// <returns></returns>
        public bool IsCheck()
        {
            return IsKingInCheck(IsMarvelTurn);
        }
        

        /// <summary>
        /// check if game is over or not
        /// </summary>
        /// <returns></returns>
        public bool GameOverStatus()
        {
            if (IsCheckMate())
            {
                IsGameOver = true;
                return true;
            }

            IsGameOver = false;
            return false;
        }
        /// <summary>
        /// check if piece belongs to marvel team by checking image path
        /// by checking keywords in the file name.
        /// </summary>
        /// <param name="pieceLocation"></param>
        /// <returns></returns>
        private bool IsMarvelPiece(string pieceLocation)
        {
            if (pieceLocation == null)
            {
                return false;
            }

            return pieceLocation.Contains("iron man") ||
                   pieceLocation.Contains("spider man") ||
                   pieceLocation.Contains("doctor strange") ||
                   pieceLocation.Contains("scarlet witch") ||
                   pieceLocation.Contains("Prof X") ||
                   pieceLocation.Contains("sheed");
        }
        /// <summary>
        /// check if piece belongs to DC team by checking image path
        /// by checking keywords in the file name.
        /// </summary>
        /// <param name="pieceLocation"></param>
        /// <returns></returns>
        private bool IsDCPiece(string pieceLocation)
        {
            if (pieceLocation == null)
            {
                return false;
            }

            return pieceLocation.Contains("batman") ||
                   pieceLocation.Contains("night wing") ||
                   pieceLocation.Contains("Raven") ||
                   pieceLocation.Contains("wonder woman") ||
                   pieceLocation.Contains("Prof K") ||
                   pieceLocation.Contains("GCPD");
        }
        /// <summary>
        /// check if selected piece belongs to current player by whose turn currently is 
        /// </summary>
        /// <param name="pieceLocation"></param>
        /// <returns></returns>
        private bool CurrentPlayerPiece(string pieceLocation)
        {
            if (IsMarvelTurn)
            {
                return IsMarvelPiece(pieceLocation);
            }
            else
            {
                return IsDCPiece(pieceLocation);
            }
        }
        // Parts of below methods are written by help of AI
        /// <summary>
        /// Create piece object based on image path stored in the board,
        /// it connects themed characters to their respective chess piece classes.
        /// </summary>
        /// <param name="pieceLocation"></param>
        /// <returns></returns>
        public Piece CreatePieceObject(string pieceLocation)
        {
            if (pieceLocation == null)
            {
                return null;
            }

            string lower = pieceLocation.ToLower();
            string team;

            // Determine team
            if (IsMarvelPiece(pieceLocation))
            {
                team = "Marvel";
            }
            else if (IsDCPiece(pieceLocation))
            {
                team = "DC";
            }
            else
            {
                return null;
            }

            // Pawn
            if (lower.Contains("sheed") || lower.Contains("gcpd"))
            {
                return new Pawn(team, pieceLocation);
            }

            // Rook
            if (lower.Contains("iron man") || lower.Contains("batman"))
            {
                return new Rook(team, pieceLocation);
            }

            // Knight
            if (lower.Contains("spider man") || lower.Contains("night wing"))
            {
                return new Knight(team, pieceLocation);
            }

            // Bishop
            if (lower.Contains("doctor strange") || lower.Contains("raven"))
            {
                return new Bishop(team, pieceLocation);
            }

            // King
            if (lower.Contains("prof x") || lower.Contains("prof k"))
            {
                return new King(team, pieceLocation);
            }

            // Queen
            if (lower.Contains("scarlet witch") || lower.Contains("wonder woman"))
            {
                return new Queen(team, pieceLocation);
            }

            return null;
        }

        /// <summary>
        /// check if both pieces belong on same side
        /// </summary>
        /// <param name="pieceOne"></param>
        /// <param name="pieceTwo"></param>
        /// <returns></returns>
        public bool OwnPiece(string pieceOne, string pieceTwo)
        {
            return (IsMarvelPiece(pieceOne) && IsMarvelPiece(pieceTwo)) ||
                   (IsDCPiece(pieceOne) && IsDCPiece(pieceTwo));
        }

        /// <summary>
        /// check if given row and column are within the 8x8 board limits
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool InsideBoard(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }

        /// <summary>
        /// validates and perform a move if allowed
        /// </summary>
        /// <param name="oldRow"></param>
        /// <param name="oldColumn"></param>
        /// <param name="newRow"></param>
        /// <param name="newColumn"></param>
        /// <returns></returns>
        public bool MoveIfValid(int oldRow, int oldColumn, int newRow, int newColumn)
        {
            if (IsGameOver)
            {
                return false;
            }

            if (!InsideBoard(oldRow, oldColumn) || !InsideBoard(newRow, newColumn))
            {
                return false;
            }

            string selectedPiece = gameBoard.GetPiece(oldRow, oldColumn);

            if (selectedPiece == null)
            {
                return false;
            }

            if (!CurrentPlayerPiece(selectedPiece))
            {
                return false;
            }

            Piece pieceObject = CreatePieceObject(selectedPiece);

            if (pieceObject == null)
            {
                return false;
            }

            string targetPiece = gameBoard.GetPiece(newRow, newColumn);
            // if king is sitting on target square, prevent the capture.
            
            if (targetPiece != null && (targetPiece.Contains("Prof X") || targetPiece.Contains("Prof K")))
            {
                return false;
            }

            bool movingMarvelPiece = IsMarvelTurn;

            // If the piece to move is king and target square has friendly piece, allow swap if it doesn't put king in check
            if (pieceObject is King && targetPiece != null && OwnPiece(selectedPiece, targetPiece))
            {
                if (!pieceObject.IsValidMove(oldRow, oldColumn, newRow, newColumn, gameBoard))
                {
                    return false;
                }

                // Swap the King piece with friendly piece.
                gameBoard.SetPiece(newRow, newColumn, selectedPiece);
                gameBoard.SetPiece(oldRow, oldColumn, targetPiece);
                
                // check if king is in still in check after swap.
                bool stillInCheckAfterSwap = IsKingInCheck(movingMarvelPiece);

                // Undo swap
                gameBoard.SetPiece(oldRow, oldColumn, selectedPiece);
                gameBoard.SetPiece(newRow, newColumn, targetPiece);

                // illegal if the king is still in check after the swap
                if (stillInCheckAfterSwap)
                {
                    return false;
                }

                // if legal, perform the swap
                gameBoard.SetPiece(newRow, newColumn, selectedPiece);
                gameBoard.SetPiece(oldRow, oldColumn, targetPiece);

                ChangeTurn();
                GameOverStatus();
                return true;
            }

            // prevent capturing your own piece
            if (targetPiece != null && OwnPiece(selectedPiece, targetPiece))
            {
                return false;
            }

            // piece movement rule
            if (!pieceObject.IsValidMove(oldRow, oldColumn, newRow, newColumn, gameBoard))
            {
                return false;
            }

            // simulate move first
            gameBoard.SetPiece(newRow, newColumn, selectedPiece);
            gameBoard.SetPiece(oldRow, oldColumn, null);

            bool stillInCheckAfterMove = IsKingInCheck(movingMarvelPiece);

            // undo simulation
            gameBoard.SetPiece(oldRow, oldColumn, selectedPiece);
            gameBoard.SetPiece(newRow, newColumn, targetPiece);

            // illegal if your king is still in check
            if (stillInCheckAfterMove)
            {
                return false;
            }

            // real move
            bool moveSuccess = gameBoard.MovePiece(oldRow, oldColumn, newRow, newColumn);

            if (moveSuccess)
            {
                ChangeTurn();
                GameOverStatus();
            }

            return moveSuccess;
        }
        /// <summary>
        /// Check if the king of the given team is in check by looping through the board to find the king's position 
        /// and then checking if any enemy piece can move to that position.
        /// </summary>
        /// <param name="isMarvelKing"></param>
        /// <returns></returns>
        private bool IsKingInCheck(bool isMarvelKing)
        {
            string[,] boardState = gameBoard.GetBoardState();
            int kingRow = -1;
            int kingColumn = -1;

            // Loop to find the king's position on the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string piece = boardState[i, j];

                    if (piece != null)
                    {
                        if (isMarvelKing && piece.Contains("Prof X"))
                        {
                            kingRow = i;
                            kingColumn = j;
                        }
                        else if (!isMarvelKing && piece.Contains("Prof K"))
                        {
                            kingRow = i;
                            kingColumn = j;
                        }
                    }
                }
            }

          
            if (kingRow == -1 || kingColumn == -1)
            {
                return false;
            }

            // Check whether any enemy piece can attack that king
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string piece = boardState[i, j];

                    if (piece == null)
                    {
                        continue;
                    }
                    // check if piece belongs to enemy team
                    bool enemyPiece;

                    if (isMarvelKing)
                    {
                        enemyPiece = IsDCPiece(piece);
                    }
                    else
                    {
                        enemyPiece = IsMarvelPiece(piece);
                    }
                    // if current piece is enemy piece, create the piece object and check if it can move to the king's position
                    if (enemyPiece)
                    {
                        Piece pieceObject = CreatePieceObject(piece);

                        if (pieceObject != null)
                        {
                            if (pieceObject.IsValidMove(i, j, kingRow, kingColumn, gameBoard))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// checkmate logic: if current player is in check and there is no valid move that can get them out of check, then it's checkmate.
        /// </summary>
        /// <returns></returns>
        public bool IsCheckMate()
        {
            // If current player is not in check, it is not checkmate
            if (!IsCheck())
            {
                return false;
            }
            // Get full board state
            string[,] boardState = gameBoard.GetBoardState();
            // Determine which king we are checking for
            bool checkingMarvelKing = IsMarvelTurn;

            // Try every piece of the current player
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string selectedPiece = boardState[i, j];

                    if (selectedPiece == null)
                    {
                        continue;
                    }
                    // skips pieces that do not belong to current player
                    if (!CurrentPlayerPiece(selectedPiece))
                    {
                        continue;
                    }

                    Piece pieceObject = CreatePieceObject(selectedPiece);

                    if (pieceObject == null)
                    {
                        continue;
                    }

                    // Try moving that piece to every square
                    for (int r = 0; r < 8; r++)
                    {
                        for (int c = 0; c < 8; c++)
                        {   // skip if move is not valid for that piece
                            if (!pieceObject.IsValidMove(i, j, r, c, gameBoard))
                            {
                                continue;
                            }
                            // set the piece on the target square.
                            string targetPiece = boardState[r, c];

                            // check if target square is empty or has enemy piece.
                            if (targetPiece == null || !OwnPiece(selectedPiece, targetPiece))
                            {
                                string originalTarget = targetPiece;
                                // simulate move
                                gameBoard.SetPiece(r, c, selectedPiece);
                                gameBoard.SetPiece(i, j, null);
                                // check if king is still in check after the move
                                bool stillInCheck = IsKingInCheck(checkingMarvelKing);

                                // undo move
                                gameBoard.SetPiece(i, j, selectedPiece);
                                gameBoard.SetPiece(r, c, originalTarget);
                                // if king is not in check after the move, then it's not checkmate
                                if (!stillInCheck)
                                {
                                    return false;
                                }
                            }
                            // King's special swap with friendly piece
                            else if (pieceObject is King)
                            {
                                //simulate swap
                                gameBoard.SetPiece(r, c, selectedPiece);
                                gameBoard.SetPiece(i, j, targetPiece);

                                bool stillInCheck = IsKingInCheck(checkingMarvelKing);

                                // undo swap
                                gameBoard.SetPiece(i, j, selectedPiece);
                                gameBoard.SetPiece(r, c, targetPiece);

                                if (!stillInCheck)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }
        public void SaveGame(string path)
        {
            MarvelDcChess.SaveGame.Save(path, gameBoard, IsMarvelTurn);
        }

        public void LoadGame(string path)
        {
            GameData data = MarvelDcChess.LoadGame.Load(path);

            if (data != null)
            {
                gameBoard.SetBoardStateFromJagged(data.Board);
                IsMarvelTurn = data.IsMarvelTurn;
            }
        }
    }
}
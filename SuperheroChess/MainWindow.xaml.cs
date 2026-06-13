using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarvelDcChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creating 2d array holding 64 button
        private Button[,] boardButtons = new Button[8, 8];
        // Creating 2d array to hold original background color.
        private Brush[,] originalBrushes = new Brush[8, 8];
        private int selectedRow = -1;
        private int selectedColumn = -1;
        private AIBot ai;
        // To hold which button is currently selected
        private Button selectedButton = null;
        private Brush selectedBrush;
        private Manager gameManager = new Manager();
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoardButtons();
            DrawBoard();
            UpdateStatusMessage();
            ai = new AIBot(gameManager);
        }

        private void InitializeBoardButtons()
        {
            // Row 0
            boardButtons[0, 0] = Button10;
            boardButtons[0, 1] = Button11;
            boardButtons[0, 2] = Button12;
            boardButtons[0, 3] = Button13;
            boardButtons[0, 4] = Button14;
            boardButtons[0, 5] = Button15;
            boardButtons[0, 6] = Button16;
            boardButtons[0, 7] = Button17;

            // Row 1
            boardButtons[1, 0] = Button20;
            boardButtons[1, 1] = Button21;
            boardButtons[1, 2] = Button22;
            boardButtons[1, 3] = Button23;
            boardButtons[1, 4] = Button24;
            boardButtons[1, 5] = Button25;
            boardButtons[1, 6] = Button26;
            boardButtons[1, 7] = Button27;

            // Row 2
            boardButtons[2, 0] = Button30;
            boardButtons[2, 1] = Button31;
            boardButtons[2, 2] = Button32;
            boardButtons[2, 3] = Button33;
            boardButtons[2, 4] = Button34;
            boardButtons[2, 5] = Button35;
            boardButtons[2, 6] = Button36;
            boardButtons[2, 7] = Button37;

            // Row 3
            boardButtons[3, 0] = Button40;
            boardButtons[3, 1] = Button41;
            boardButtons[3, 2] = Button42;
            boardButtons[3, 3] = Button43;
            boardButtons[3, 4] = Button44;
            boardButtons[3, 5] = Button45;
            boardButtons[3, 6] = Button46;
            boardButtons[3, 7] = Button47;

            // Row 4
            boardButtons[4, 0] = Button50;
            boardButtons[4, 1] = Button51;
            boardButtons[4, 2] = Button52;
            boardButtons[4, 3] = Button53;
            boardButtons[4, 4] = Button54;
            boardButtons[4, 5] = Button55;
            boardButtons[4, 6] = Button56;
            boardButtons[4, 7] = Button57;

            // Row 5
            boardButtons[5, 0] = Button60;
            boardButtons[5, 1] = Button61;
            boardButtons[5, 2] = Button62;
            boardButtons[5, 3] = Button63;
            boardButtons[5, 4] = Button64;
            boardButtons[5, 5] = Button65;
            boardButtons[5, 6] = Button66;
            boardButtons[5, 7] = Button67;

            // Row 6
            boardButtons[6, 0] = Button70;
            boardButtons[6, 1] = Button71;
            boardButtons[6, 2] = Button72;
            boardButtons[6, 3] = Button73;
            boardButtons[6, 4] = Button74;
            boardButtons[6, 5] = Button75;
            boardButtons[6, 6] = Button76;
            boardButtons[6, 7] = Button77;

            // Row 7
            boardButtons[7, 0] = Button80;
            boardButtons[7, 1] = Button81;
            boardButtons[7, 2] = Button82;
            boardButtons[7, 3] = Button83;
            boardButtons[7, 4] = Button84;
            boardButtons[7, 5] = Button85;
            boardButtons[7, 6] = Button86;
            boardButtons[7, 7] = Button87;
            // Storing original background color of buttons in 2d array
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    originalBrushes[i, j] = boardButtons[i, j].Background;
                }
            }
        }
        // Parts of below methods were written with help of AI
        /// <summary>
        /// To update the UI based on current board state
        /// </summary>
        private void DrawBoard()
        {   // Getting current board state from board class by referening board state array
            string[,] boardStateRefer = gameManager.GetBoard().GetBoardState();
            // Looping through all row and columns
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {   // if there is no piece at this position, clear the button content
                    if (boardStateRefer[i, j] == null)
                    {
                        boardButtons[i, j].Content = null;
                    }
                    // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/image
                    // https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage?view=windowsdesktop-10.0
                    else
                    {   // Creating new image control
                        Image image = new Image();
                        // Load the image from file path stored in boardstate
                        image.Source = new BitmapImage(new Uri(boardStateRefer[i, j], UriKind.Relative));
                        // set the image as the content of the button
                        boardButtons[i, j].Content = image;
                    }
                }
            }
        }
        /// <summary>
        /// Handling button click by looping through all the buttons on board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Convert clicked element to button
            Button clickedButton = (Button)sender;
            // Store clicked button position
            int row = -1;
            int column = -1;

            // Find clicked button position
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardButtons[i, j] == clickedButton)
                    {
                        row = i;
                        column = j;
                    }
                }
            }
            // if button not found, exit method
            if (row == -1 || column == -1)
            {
                return;
            }

            // if not piece is selected
            if (selectedRow == -1)
            {   // Get piece at clicked position
                string selectedPiece = gameManager.GetBoard().GetPiece(row, column);
                // if there is a piece, select it and highlight valid moves
                if (selectedPiece != null)
                {
                    selectedRow = row;
                    selectedColumn = column;
                    selectedButton = clickedButton;
                    selectedBrush = clickedButton.Background;
                    clickedButton.Background = Brushes.Green;
                    // Create piece object to check valid moves
                    Piece pieceObject = gameManager.CreatePieceObject(selectedPiece);

                    if (pieceObject != null)
                    {
                        for (int r = 0; r < 8; r++)
                        {
                            for (int c = 0; c < 8; c++)
                            {
                                // skip same square
                                if (r == selectedRow && c == selectedColumn)
                                {
                                    continue;
                                }

                                string targetPiece = gameManager.GetBoard().GetPiece(r, c);

                                // skip own piece
                                if (targetPiece != null && gameManager.OwnPiece(selectedPiece, targetPiece))
                                {
                                    continue;
                                }

                                // highlight valid move
                                if (pieceObject.IsValidMove(selectedRow, selectedColumn, r, c, gameManager.GetBoard()))
                                {
                                    boardButtons[r, c].Background = Brushes.Gold;
                                }
                            }
                        }
                    }
                }

                return;
            }

            // if same piece is clicked again, deselect and clear highlights
            if (selectedRow == row && selectedColumn == column)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        boardButtons[i, j].Background = originalBrushes[i, j];
                    }
                }

                selectedRow = -1;
                selectedColumn = -1;
                selectedButton = null;
                selectedBrush = null;
                return;
            }

            // move piece if valid
            bool moved = gameManager.MoveIfValid(selectedRow, selectedColumn, row, column);

            if (moved)
            {
                DrawBoard();
                UpdateStatusMessage();
                // false = DC
                if (!gameManager.GetCurrentTurn())
                {
                    ai.MakeMove();
                    DrawBoard();
                    UpdateStatusMessage();
                }
            }
           
            // clear highlights
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardButtons[i, j].Background = originalBrushes[i, j];
                }
            }

            // reset selection
            selectedRow = -1;
            selectedColumn = -1;
            selectedButton = null;
            selectedBrush = null;

        }
        private void UpdateStatusMessage()
        {
            bool isMarvelTurn = gameManager.GetCurrentTurn();

            if (gameManager.IsCheckMate())
            {
                if (isMarvelTurn)
                {
                    StatusMessage.Text = "Marvel Checkmate!";
                }
                else
                {
                    StatusMessage.Text = "DC Checkmate!";
                }
            }
            else if (gameManager.IsCheck())
            {
                if (isMarvelTurn)
                {
                    StatusMessage.Text = "Marvel in Check";
                }
                else
                {
                    StatusMessage.Text = "DC in Check";
                }
            }
            else
            {
                if (isMarvelTurn)
                {
                    StatusMessage.Text = "Marvel Turn";
                }
                else
                {
                    StatusMessage.Text = "DC Turn";
                }
            }
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            // Reset board state 
            gameManager.GetBoard().InitializeBoard();
            gameManager.StartGame();
            // Redraw UI
            DrawBoard();

            // Clear selection
            selectedRow = -1;
            selectedColumn = -1;
            selectedButton = null;
            selectedBrush = null;
            UpdateStatusMessage();

            // Clear highlights
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardButtons[i, j].Background = originalBrushes[i, j];
                }
            }
        }

        private void StatusMessage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            gameManager.SaveGame("savegame.json");
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            gameManager.LoadGame("savegame.json");
            DrawBoard();
            UpdateStatusMessage();
        }
    }
} 
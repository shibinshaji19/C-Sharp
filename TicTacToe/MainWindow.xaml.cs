/* Author: Shibin Shaji
 * Date: September 25, 2025
 * Description: This program is game of tictactoe */
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

namespace TicTacToeGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {// Boolean to check players turn
        bool Player1Turn = true;
     // Variable to count the scores
        double scorePlayer1 = 0;
        double scorePlayer2 = 0;
        double draw = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
        // Event handler that manages when a square is clicked and disables them.
        private void BoxClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            
            if (Player1Turn)
            {
                clickedButton.Content = "X";
                Player1Turn = false;
                currentPlayer.Text = playerNameX.Text;
            }
            else
            {
                clickedButton.Content = "O";
                Player1Turn = true;
                currentPlayer.Text = playerNameO.Text;
            }
            clickedButton.IsEnabled = false;

            if (CheckAllLines() == true)
            {
                
             // Calculating the scores and display appropriate messages
                if (Player1Turn)
                {
                    scorePlayer2++;
                    scorePlayerO.Text = scorePlayer2.ToString();
                    MessageBox.Show("Player 2 wins!");
                }

                else
                {
                    scorePlayer1++;
                    scorePlayerX.Text = scorePlayer1.ToString();
                    MessageBox.Show("Player 1 wins");
                }
                ResetSquares();
            }
            else if (CheckDrawGame())
                
            {
               
                draw++;
                tieGameBox.Text = draw.ToString();
                MessageBox.Show("Game is tied");
                ResetSquares();

            }
 
            }
            // Function to check if three buttons are in a line to get a winner
            private bool CheckLine(Button buttonX, Button buttonY, Button buttonZ)
            {
                if (buttonX.Content == buttonY.Content && buttonY.Content == buttonZ.Content && buttonX.Content !=null)
            {
                buttonX.Foreground = Brushes.DarkRed;
                buttonY.Foreground = Brushes.DarkRed;
                buttonZ.Foreground = Brushes.DarkRed;

                return true;
            }
            else 
            {
                return false;
            }
        }
        // Function to check all the possibilities of buttons
        private bool CheckAllLines()
        {
            return CheckLine(button1, button2, button3) ||
                   CheckLine(button4, button5, button6) ||
                   CheckLine(button7, button8, button9) ||
                   CheckLine(button1, button4, button7) ||
                   CheckLine(button2, button5, button8) ||
                   CheckLine(button3, button6, button9) ||
                   CheckLine(button1, button5, button9) ||
                   CheckLine(button3, button5, button7);

        }
        // To check if a game is tied
        private bool CheckDrawGame()
        {
            return  button1.IsEnabled == false &&
                    button2.IsEnabled == false &&
                    button3.IsEnabled == false &&
                    button4.IsEnabled == false &&
                    button5.IsEnabled == false &&
                    button6.IsEnabled == false &&
                    button7.IsEnabled == false &&
                    button8.IsEnabled == false &&
                    button9.IsEnabled == false;

        }
        // To reset all buttons after a winner
        private void ResetSquares()
        {
            button1.Content = null;
            button2.Content = null;
            button3.Content = null;
            button4.Content = null;
            button5.Content = null;
            button6.Content = null;
            button7.Content = null;
            button8.Content = null;
            button9.Content = null;

       
            button1.IsEnabled = true;
            button2.IsEnabled = true;
            button3.IsEnabled = true;
            button4.IsEnabled = true;
            button5.IsEnabled = true;
            button6.IsEnabled = true;
            button7.IsEnabled = true;
            button8.IsEnabled = true;
            button9.IsEnabled = true;

            button1.Foreground = Brushes.Gray;
            button2.Foreground = Brushes.Gray;
            button3.Foreground = Brushes.Gray;
            button4.Foreground = Brushes.Gray; 
            button5.Foreground = Brushes.Gray;
            button6.Foreground = Brushes.Gray;
            button7.Foreground = Brushes.Gray;
            button8.Foreground = Brushes.Gray;
            button9.Foreground = Brushes.Gray;

            Player1Turn = true;
            currentPlayer.Text = playerNameX.Text;
        }
        // To close the application
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm Close", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        // To full reset the game includes text fields, buttons, color of contents.
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            playerNameX.Clear();
            playerNameO.Clear();
            currentPlayer.Clear();
            scorePlayerO.Clear();
            scorePlayerX.Clear();
            tieGameBox.Clear();
            scorePlayer1 = 0;
            scorePlayer2 = 0;
            draw = 0;
            button1.Content = null;
            button2.Content = null;
            button3.Content = null;
            button4.Content = null;
            button5.Content = null;
            button6.Content = null;
            button7.Content = null;
            button8.Content = null;
            button9.Content = null;

            button1.IsEnabled = true;
            button2.IsEnabled = true;
            button3.IsEnabled = true;
            button4.IsEnabled = true;
            button5.IsEnabled = true;
            button6.IsEnabled = true;
            button7.IsEnabled = true;
            button8.IsEnabled = true;
            button9.IsEnabled = true;

            button1.Foreground = Brushes.Gray;
            button2.Foreground = Brushes.Gray;
            button3.Foreground = Brushes.Gray;
            button4.Foreground = Brushes.Gray;
            button5.Foreground = Brushes.Gray;
            button6.Foreground = Brushes.Gray;
            button7.Foreground = Brushes.Gray;
            button8.Foreground = Brushes.Gray;
            button9.Foreground = Brushes.Gray;

            playerNameX.Focus();
        }
    }
}
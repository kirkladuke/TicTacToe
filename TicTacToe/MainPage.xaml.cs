using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace TicTacToe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        

        // When True = x turn, false = y turn;
        bool turn = true;
        // COunt turns to end game
        int turnCount = 0;
        // Mark who is winner
        string winner = "";
        bool singlePlayer = true;
        bool thereIsAWinner = false;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void button_click(object sender, RoutedEventArgs e)
        {

            // Will Put X or O (depending on player) to button text.  
            Button b = (Button)sender;
            if (turn)

                b.Content = "X";
                
            else
                b.Content = "O";
            // Will change variable turn to false or true, depending on current selection
            turn = !turn;

            // Turn off Button once used
            b.IsEnabled = false;

            // Turn counter to see if all squares filled and draw
            turnCount++;

            // Run Methof to see if there is a winner
            checkForWinner(); 

            // Set button box to say who's turn it is
            setCurrentUserText();

            if ((!turn) && (singlePlayer))
            {
                computerTurn();
            }

        }
        //Set up AI
        private void computerTurn()
        {
            // My Priority Logic

            // Priority 1: Get Tic Tac Toe
            // Priority 2: Block Tic Tac Toe
            // Priority 3: Corner Space
            // Priority 4: Open Space

            Button move = null;

            //Try Tic Tac Toe
            move = winOrBlockTry("O");
            if (move == null)
            {
                move = winOrBlockTry("X");
                if (move == null)
                {
                    move = cornertry();
                    if (move == null)
                    {
                        move = openSpaceTry();
                    }//end if
                }// end if
            }// end if

           // move.Tag = move.GetType();
           // string play = move.Tag.ToString();

            if (thereIsAWinner == false)
            {
                if (move == null)
                { }
                else
                {
                    ButtonAutomationPeer peer =
               new ButtonAutomationPeer(move);
                    IInvokeProvider invokeProv =
                      peer.GetPattern(PatternInterface.Invoke)
                      as IInvokeProvider;
                    invokeProv.Invoke();
                }
            }

            else
            {
               
            }


        }


        


        private Button openSpaceTry()
        {
            if (Convert.ToString(butA1.Content) == "")
                return butA1;
            if (Convert.ToString(butA2.Content) == "")
                return butA2;
            if (Convert.ToString(butA3.Content) == "")
                return butA3;

            if (Convert.ToString(butB1.Content) == "")
                return butB1;
            if (Convert.ToString(butB2.Content) == "")
                return butB2;
            if (Convert.ToString(butB3.Content) == "")
                return butB3;

            if (Convert.ToString(butC1.Content) == "")
                return butC1;
            if (Convert.ToString(butC2.Content) == "")
                return butC2;
            if (Convert.ToString(butC3.Content) == "")
                return butC3;

            return null;
            
        }

        private Button cornertry()
        {
            //if (butA1.Content as string == "O")
            //{
            //    if (butA3.Content as string == "")
            //        return butA3;
            //    if (butC3.Content as string == "")
            //        return butC3;
            //    if (butC1.Content as string == "")
            //        return butC1;
            //}

            //if (butA3.Content as string == "O")
            //{
            //    if (butA1.Content as string == "")
            //        return butA1;
            //    if (butC3.Content as string == "")
            //        return butC3;
            //    if (butC1.Content as string == "")
            //        return butC1;
            //}

            //if (butC3.Content as string == "O")
            //{
            //    if (butA1.Content as string == "")
            //        return butA1;
            //    if (butA3.Content as string == "")
            //        return butA3;
            //    if (butC1.Content as string == "")
            //        return butC1;
            //}

            //if (butC1.Content as string == "O")
            //{
            //    if (butA1.Content as string == "")
            //        return butA1;
            //    if (butA3.Content as string == "")
            //        return butA3;
            //    if (butC3.Content as string == "")
            //        return butC3;
            //}

            // Take first available corner
            if (Convert.ToString(butA1.Content) == "")
                return butA1;
            if (Convert.ToString(butA3.Content)  == "")
                return butA3;
            if (Convert.ToString(butC1.Content) == "")
                return butC1;
            if (Convert.ToString(butC3.Content) == "")
                return butC3;

            return null;
        }

        

        private Button winOrBlockTry(string mark)
        { 
        // Horizontal try's
            if ((Convert.ToString(butA1.Content) == mark) && (Convert.ToString(butA2.Content) == mark) && (Convert.ToString(butA3.Content) == ""))
                return butA3;
            if ((Convert.ToString(butA2.Content) == mark) && (Convert.ToString(butA3.Content) == mark) && (Convert.ToString(butA1.Content) == ""))
                return butA1;
            if ((Convert.ToString(butA1.Content) == mark) && (Convert.ToString(butA3.Content) == mark) && (Convert.ToString(butA2.Content) == ""))
                return butA2;

            if ((Convert.ToString(butB1.Content) == mark) && (Convert.ToString(butB2.Content) == mark) && (Convert.ToString(butB3.Content) == ""))
                return butB3;
            if ((Convert.ToString(butB2.Content) == mark) && (Convert.ToString(butB3.Content) == mark) && (Convert.ToString(butB1.Content) == ""))
                return butB1;
            if ((Convert.ToString(butB1.Content) == mark) && (Convert.ToString(butB3.Content) == mark) && (Convert.ToString(butB2.Content) == ""))
                return butB2;

            if ((Convert.ToString(butC1.Content) == mark) && (Convert.ToString(butC2.Content) == mark) && (Convert.ToString(butC3.Content) == ""))
                return butC3;
            if ((Convert.ToString(butC2.Content) == mark) && (Convert.ToString(butC3.Content) == mark) && (Convert.ToString(butC1.Content) == ""))
                return butC1;
            if ((Convert.ToString(butC1.Content) == mark) && (Convert.ToString(butC3.Content) == mark) && (Convert.ToString(butC2.Content) == ""))
                return butC2;

            //vertical try's
            if ((Convert.ToString(butA1.Content) == mark) && (Convert.ToString(butB1.Content) == mark) && (Convert.ToString(butC1.Content) == ""))
                return butC1;
            if ((Convert.ToString(butB1.Content) == mark) && (Convert.ToString(butC1.Content) == mark) && (Convert.ToString(butA1.Content) == ""))
                return butA1;
            if ((Convert.ToString(butA1.Content) == mark) && (Convert.ToString(butC1.Content) == mark) && (Convert.ToString(butB1.Content) == ""))
                return butB1;

            if ((Convert.ToString(butA2.Content) == mark) && (Convert.ToString(butB2.Content) == mark) && (Convert.ToString(butC2.Content) == ""))
                return butC2;
            if ((Convert.ToString(butB2.Content) == mark) && (Convert.ToString(butC2.Content) == mark) && (Convert.ToString(butA2.Content) == ""))
                return butA2;
            if ((Convert.ToString(butA2.Content) == mark) && (Convert.ToString(butC2.Content) == mark) && (Convert.ToString(butB2.Content) == ""))
                return butB2;

            if ((Convert.ToString(butA3.Content) == mark) && (Convert.ToString(butB3.Content) == mark) && (Convert.ToString(butC3.Content) == ""))
                return butC3;
            
            if ((Convert.ToString(butB3.Content) == mark) && (Convert.ToString(butC3.Content) == mark) && (Convert.ToString(butA3.Content) == ""))
                return butA3;
            if ((Convert.ToString(butA3.Content) == mark) && (Convert.ToString(butC3.Content) == mark) && (Convert.ToString(butB3.Content) == ""))
                return butB3;

            //Diagonal try's
            if ((Convert.ToString(butA1.Content) == mark) && (Convert.ToString(butB2.Content) == mark) && (Convert.ToString(butC3.Content) == ""))
                return butC3;
            if ((Convert.ToString(butB2.Content) == mark) && (Convert.ToString(butC3.Content) == mark) && (Convert.ToString(butA1.Content) == ""))
                return butA1;
            if ((Convert.ToString(butA1.Content) == mark) && (Convert.ToString(butC3.Content) == mark) && (Convert.ToString(butB2.Content) == ""))
                return butB2;

            if ((Convert.ToString(butA3.Content) == mark) && (Convert.ToString(butC1.Content) == mark) && (Convert.ToString(butB2.Content) == ""))
                return butB2;
           
            if ((Convert.ToString(butB2.Content) == mark) && (Convert.ToString(butC1.Content) == mark) && (Convert.ToString(butA3.Content) == ""))
                return butA3;
            if ((Convert.ToString(butA3.Content) == mark) && (Convert.ToString(butC1.Content) == mark) && (Convert.ToString(butB2.Content) == ""))
                return butB2;

            return null;
        }

        private void setCurrentUserText()
        {
            if (turn)
                btnCurrentPlayer.Content = "Player X's turn";
            else
                btnCurrentPlayer.Content = "Player O's turn";
        }

         private async void showWinner()
                {
                //Creating instance for the MessageDialog Class  
                //and passing the message in it's Constructor  
                    MessageDialog msgbox = new MessageDialog(winner + "   Wins", "congratulations!!");
                //Calling the Show method of MessageDialog class  
                //which will show the MessageBox  
               
                await msgbox.ShowAsync();
                }

         private async void showDraw()
         {
             //Creating instance for the MessageDialog Class  
             //and passing the message in it's Constructor  
             MessageDialog msgbox = new MessageDialog("Draw", "Bummer!");
             //Calling the Show method of MessageDialog class  
             //which will show the MessageBox  

             await msgbox.ShowAsync();
         }

        private  void checkForWinner()
        {
       
             
            // See if a winner variable bool
            


            //Horizontal winner check
            if ((butA1.Content as string == butA2.Content as string) && (butA2.Content as string == butA3.Content as string) && (!butA1.IsEnabled))   
            { thereIsAWinner = true; }
            else if ((butB1.Content as string == butB2.Content as string) && (butB2.Content as string == butB3.Content as string) && (!butB1.IsEnabled))   
            { thereIsAWinner = true; }
            else if ((butC1.Content as string == butC2.Content as string) && (butC2.Content as string == butC3.Content as string) && (!butC1.IsEnabled))   
            { thereIsAWinner = true; }

            // Vertical winner check
            if ((butA1.Content as string == butB1.Content as string) && (butB1.Content as string == butC1.Content as string) && (!butA1.IsEnabled))
            { thereIsAWinner = true; }
            else if ((butA2.Content as string == butB2.Content as string) && (butB2.Content as string == butC2.Content as string) && (!butA2.IsEnabled))
            { thereIsAWinner = true; }
            else if ((butA3.Content as string == butB3.Content as string) && (butB3.Content as string == butC3.Content as string) && (!butA3.IsEnabled))
            { thereIsAWinner = true; }

            // Diagonal winner check
            if ((butA1.Content as string == butB2.Content as string) && (butB2.Content as string == butC3.Content as string) && (!butA1.IsEnabled))
            { thereIsAWinner = true; }
            else if ((butA3.Content as string == butB2.Content as string) && (butB2.Content as string == butC1.Content as string) && (!butA3.IsEnabled))
            { thereIsAWinner = true; }
            

            if (thereIsAWinner == true)
            {

                if (turn)
                {
                    winner = "O";
                    // add one to O player count
                    tblOCount.Text = (Int32.Parse(tblOCount.Text) + 1).ToString();
                }
                else 
                {
                    winner = "X";
                    // add one to X player count
                    tblXCount.Text = (Int32.Parse(tblXCount.Text) + 1).ToString();
                }
                showWinner();

                disableButtons();


            }

            // See if game ends in draw
            else 
            {
                if (turnCount == 9)
                { 
                    showDraw();
                    // add one to draw count
                    tblDrawCount.Text = (Int32.Parse(tblDrawCount.Text) + 1).ToString();
                }
            }


        
        }// End CheckForWinner

        private void disableButtons()
        {
            butA1.IsEnabled = false;
            butA2.IsEnabled = false;
            butA3.IsEnabled = false;
            butB1.IsEnabled = false;
            butB2.IsEnabled = false;
            butB3.IsEnabled = false;
            butC1.IsEnabled = false;
            butC2.IsEnabled = false;
            butC3.IsEnabled = false;
        }
        
        private void enableButtons()
        {
            // Will enable all the buttons to be clickable again
            butA1.IsEnabled = true;
            butA2.IsEnabled = true;
            butA3.IsEnabled = true;
            butB1.IsEnabled = true;
            butB2.IsEnabled = true;
            butB3.IsEnabled = true;
            butC1.IsEnabled = true;
            butC2.IsEnabled = true;
            butC3.IsEnabled = true;
        }
        private void resetButtonText()
        {
            // Will reset button text to blank for new game
            butA1.Content = "";
            butA2.Content = "";
            butA3.Content = "";
            butB1.Content = "";
            butB2.Content = "";
            butB3.Content = "";
            butC1.Content = "";
            butC2.Content = "";
            butC3.Content = "";
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            turn = true;
            turnCount = 0;
            enableButtons();
            resetButtonText();
            btnCurrentPlayer.Content = "Player X's turn";
            thereIsAWinner = false;
        }

        private void btnResetScore_Click(object sender, RoutedEventArgs e)
        {

            // Will reset Scores
            tblXCount.Text = "0";
            tblOCount.Text = "0";
            tblDrawCount.Text = "0";
        }

        

        private void gameModeClick2(object sender, RoutedEventArgs e)
        {
            singlePlayer = false;
            turn = true;
            turnCount = 0;
            enableButtons();
            resetButtonText();
            btnCurrentPlayer.Content = "Player X's turn";
            thereIsAWinner = false;
        }

        private void gameModeClick1(object sender, RoutedEventArgs e)
        {
            singlePlayer = true;
            turn = true;
            turnCount = 0;
            enableButtons();
            resetButtonText();
            btnCurrentPlayer.Content = "Player X's turn";
            thereIsAWinner = false;
        }

       

        

       


    }
}

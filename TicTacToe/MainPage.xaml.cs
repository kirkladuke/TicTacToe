using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            // Run Methof to see if there is a winner
            checkForWinner(); 

        }

         private async void show()
                {
                //Creating instance for the MessageDialog Class  
                //and passing the message in it's Constructor  
                MessageDialog msgbox = new MessageDialog("You Have Won");
                //Calling the Show method of MessageDialog class  
                //which will show the MessageBox  
               
                await msgbox.ShowAsync();
                }

        private  void checkForWinner()
        {
           


             
            // See if a winner variable bool
            bool thereIsAWinner = false;


            //Horizontal winner check
            if ((butA1.Content as string == butA2.Content as string) && (butA2.Content as string == butA3.Content as string) && (!butA1.IsEnabled))   
            { thereIsAWinner = true; }
            else if ((butB1.Content as string == butB2.Content as string) && (butB2.Content as string == butB3.Content as string) && (!butB1.IsEnabled))   
            { thereIsAWinner = true; }
            else if ((butC1.Content as string == butC2.Content as string) && (butC2.Content as string == butC3.Content as string) && (!butC1.IsEnabled))   
            { thereIsAWinner = true; }

            if (thereIsAWinner == true)
            {
                show();
                butC1.IsEnabled = false;
            }  
        
        }// End CheckForWinner

       


    }
}

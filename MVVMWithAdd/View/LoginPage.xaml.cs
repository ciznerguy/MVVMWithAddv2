using MVVMWithAdd.Model;
using MVVMWithAdd.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MVVMWithAdd.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;

        public LoginPage(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;

           
        }

       

        private void CheckUser()
        {
            // Get the entered email and password 
            string enteredEmail = UsrTxtBox.Text;
            string enteredPassword = PwdBox.Password;

            // Search for a match in the list
            User matchedUser = null;

            foreach (User user in _sharedViewModel.UsersList)
            {
                if (user.Email == enteredEmail && user.Password==enteredPassword)
                {
                    matchedUser = user;
                    break; // Found a match, exit the loop
                }
            }
        }
        private void UsrTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text when the TextBox gets focus
            UsrTxtBox.Text = "";
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            
            try
            {
                string uri = e.Uri.ToString();
                
                // Load AddUser page into the mainFrame
                if (uri == "/View/RegisterNew.xaml")
                {
                    


                    // Navigate to the AddUser page directly
                    if (NavigationService != null && uri == "/View/RegisterNew.xaml")
                    {
                      
                        RegisterNew register = new RegisterNew(_sharedViewModel);
                        NavigationService.Navigate(register);
                        MessageBox.Show("Navigated to RegisterNew");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while navigating: {ex.Message}");
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the entered email and password 
                string enteredEmail = UsrTxtBox.Text;
                string enteredPassword = PwdBox.Password;
                
               
                User matchedUser = null;

                // Search for a match in the list
                foreach (User user in _sharedViewModel.UsersList)
                {
                    bool check = user.Password == enteredPassword;
                   // MessageBox.Show(enteredPassword + " " + user.Password+ " " + check);
                    if (check)
                    {
                        matchedUser = user;
                        break; // Found a match, exit the loop
                    }
                }



                // Check if a match was found and load the main page
                if (matchedUser != null) 
                        {  // show the main page
                    MainPage MainPage = new MainPage(_sharedViewModel);
                    NavigationService.Navigate(MainPage);
                    Application.Current.Windows[0].Height = 700;
                    Application.Current.Windows[0].Width = 1600;
                }
               
                else
                {
                    // No match found, display an error message
                    MessageBox.Show("Invalid email or password. Please try again."+enteredEmail);
                    
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                MessageBox.Show($"An error occurred: {ex.Message}");
            }


        }
    }
}

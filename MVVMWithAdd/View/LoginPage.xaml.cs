using MVVMWithAdd.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private void UsrTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text when the TextBox gets focus
            UsrTxtBox.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the entered email and password
                string enteredEmail = UsrTxtBox.Text;
                string enteredPassword = PwdBox.Password;

                // Search for a match in the list
                var matchedUser = _sharedViewModel.UsersList
                    .FirstOrDefault(user => user.Email == enteredEmail && user.Password == enteredPassword);

                // Check if a match was found and load the main page
                if (matchedUser != null) 
                        {  // show the main page
                    MainPage MainPage = new MainPage(_sharedViewModel);
                    NavigationService.Navigate(MainPage);
                }
               
                else
                {
                    // No match found, display an error message
                    MessageBox.Show("Invalid email or password. Please try again.");
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

using MVVMWithAdd.Model;
using MVVMWithAdd.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Data.SQLite;
using System.Runtime.Remoting.Messaging;

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

                // Updated connection string with your database path
                string connectionString = @"Data Source=C:\Users\User\source\repos\פרויקט 5 יחידות\MVVMWithAdd\myfirstsql.db;Version=3;";
                bool userFound = false; // Flag to indicate if user was found

                using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to search for user
                    string sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password LIMIT 1";
                    using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", enteredEmail);
                        command.Parameters.AddWithValue("@Password", enteredPassword);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // If a user is found
                            {
                                userFound = true; // Set the flag to true
                            }
                        }
                    }
                }

                // Check if a match was found and load the main page
                if (userFound)
                {
                    // User found, navigate to the MainPage
                    MainPage mainPage = new MainPage(_sharedViewModel);
                    NavigationService.Navigate(mainPage);
                    Application.Current.Windows[0].Height = 700;
                    Application.Current.Windows[0].Width = 1600;
                }
                else
                {
                    // No match found, display an error message
                    MessageBox.Show("Invalid email or password. Please try again." + enteredEmail);
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

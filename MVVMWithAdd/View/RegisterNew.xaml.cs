using MVVMWithAdd.Model;
using MVVMWithAdd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMWithAdd.View
{

    public partial class RegisterNew : Page
    {
        public static SharedViewModel _sharedViewModel;

        public RegisterNew(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;
        
            if (_sharedViewModel.UsersList == null)
            {
                _sharedViewModel.UsersList = new List<User>();
            }
            MessageBox.Show("HI " + _sharedViewModel.UsersList.Count);
        }



        public RegisterNew()
        {
            InitializeComponent();
        }

        //generate method to receive a string and return true if this is a correct email structure
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        // generate a method to check if the email is already in the list
        public static bool IsEmailInList(string email)
        {
            foreach (User user in _sharedViewModel.UsersList)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }
            return false;
        }

        // legal password is 6 digits long or more and contains at least one digit and one letter
        public static bool IsValidPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                return false;
            }
            if (!password.Any(char.IsLetter))
            {
                return false;
            }
            return true;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string firstPassword = txtPassword1.Text;
            string secondPassword = txtPassword2.Text;

            MessageBox.Show("HI ");

            if (firstPassword == secondPassword)
            {
                User newUser = new User
                {
                    FirstName = txtFname.Text,
                    LastName = txtLname.Text,
                    City = txtCity.Text,
                    State = "nj",
                    Country = "Israel",
                    Email = txtEmail.Text,
                    Password = txtPassword1.Text,
                };
              
                if (!IsValidEmail(newUser.Email))
                {
                    MessageBox.Show("Invalid email");
                    return;
                }
    
                if (IsEmailInList(newUser.Email))
                {
                    MessageBox.Show("Email already exists");
                    return;
                }
                if (!IsValidPassword(newUser.Password))
                {
                    MessageBox.Show("Invalid password");
                    return;
                }
               
                if (_sharedViewModel.UsersList != null)
                {
                    _sharedViewModel.UsersList.Add(newUser);
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

           
                    if (NavigationService != null)
                    {
                       LoginPage login = new LoginPage(_sharedViewModel);
                        NavigationService.Navigate(login);
                        MessageBox.Show("Navigated to Login");
                    }
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match");
            }
        }

    }
}

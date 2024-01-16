using MVVMWithAdd.Model;
using MVVMWithAdd.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MVVMWithAdd.View
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        private SharedViewModel _sharedViewModel;

        public AddUser(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;
           
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get data from textboxes
                int userId = int.Parse(txtUserId.Text);
                string firstName = txtFname.Text;
                string lastName = txtLname.Text;
                string city = txtCity.Text;
                string email = txtEmail.Text;

                // Create a new user
                User newUser = new User
                {
                    UserId = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    State = "",
                    Country = "",
                    Email = email,                   
                    Password ="",
                    /*
                  
                        private int userId;
                        private string firstName;
                        private string lastName;
                        private string city;
                        private string state;
                        private string country;
                        private string eMail;
                        private string password;*/
                };

                _sharedViewModel.UsersList.Add(newUser);
                MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                // Optionally, clear the textboxes after adding the user
                ClearTextBoxes();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void ClearTextBoxes()
        {
            // Clear the content of all textboxes
            txtUserId.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtCity.Clear();
            txtEmail.Clear();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NavigationService != null)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("NavigationService is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error hh", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
}

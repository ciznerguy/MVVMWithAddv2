
using MVVMWithAdd.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite; // Make sure to add this using directive
using System.Runtime.CompilerServices;

namespace MVVMWithAdd.ViewModel
    {
        class UserViewModel : INotifyPropertyChanged
        {
            private SharedViewModel _sharedViewModel;

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public UserViewModel(SharedViewModel sharedViewModel)
            {
                _sharedViewModel = sharedViewModel;
                LoadUsersFromDatabase(); // Load users from the database instead of using hardcoded values
            }
            public void RefreshUsers()
            {
                LoadUsersFromDatabase(); // Reuse the method that loads users from the database
            }


            private void LoadUsersFromDatabase()
            {
                // Update the path to your actual database file
                string connectionString = @"Data Source=C:\Users\User\source\repos\פרויקט 5 יחידות\MVVMWithAdd\myfirstsql.db;Version=3;";
                var usersList = new List<User>();

             using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Users";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersList.Add(new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    FirstName = Convert.ToString(reader["FirstName"]),
                                    LastName = Convert.ToString(reader["LastName"]),
                                    City = Convert.ToString(reader["City"]),
                                    State = Convert.ToString(reader["State"]),
                                    Country = Convert.ToString(reader["Country"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    Password = Convert.ToString(reader["Password"]),
                                });
                            }
                        }
                    }
                }

                _sharedViewModel.UsersList = usersList; // Update the SharedViewModel's UsersList
                OnPropertyChanged(nameof(Users)); // Notify that the Users list has been updated
            }

            public List<User> Users
            {
                get { return _sharedViewModel.UsersList; }
            }
        }
    }




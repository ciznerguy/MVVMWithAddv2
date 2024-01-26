// UserViewModel.cs

using MVVMWithAdd.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public void RefreshUsers()
        {
            // Don't need to reload data, just notify UI
            OnPropertyChanged(nameof(Users));
        }
        public UserViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;

            if (_sharedViewModel.UsersList == null || _sharedViewModel.UsersList.Count == 0)
            {
                _sharedViewModel.UsersList = new List<User>
{
                new User { UserId = 1, FirstName = "אביגיל", LastName = "כהן", City = "תל אביב", State = "TA", Country = "ישראל", Email = "1", Password = "1" },
                new User { UserId = 2, FirstName = "משה", LastName = "לוי", City = "ירושלים", State = "JM", Country = "ישראל", Email = "moshe@example.com", Password = "1234" },
                new User { UserId = 3, FirstName = "שרה", LastName = "גולן", City = "חיפה", State = "HA", Country = "ישראל", Email = "sarah@example.com", Password = "password3" },
                new User { UserId = 4, FirstName = "יעקב", LastName = "פרץ", City = "באר שבע", State = "BS", Country = "ישראל", Email = "t@test.com", Password = "1234" },
                new User { UserId = 5, FirstName = "רחל", LastName = "מאיר", City = "אשדוד", State = "ASD", Country = "ישראל", Email = "rachel@example.com", Password = "password5" },
                new User { UserId = 6, FirstName = "דוד", LastName = "גור", City = "נתניה", State = "NT", Country = "ישראל", Email = "david@example.com", Password = "password6" },
                new User { UserId = 7, FirstName = "מרים", LastName = "זכאי", City = "עפולה", State = "AF", Country = "ישראל", Email = "miriam@example.com", Password = "password7" },
                new User { UserId = 8, FirstName = "עידו", LastName = "אביטל", City = "אילת", State = "IL", Country = "ישראל", Email = "ido@example.com", Password = "password8" },
                new User { UserId = 9, FirstName = "תמר", LastName = "פלאי", City = "טבריה", State = "TB", Country = "ישראל", Email = "tamar@example.com", Password = "password9" },
                new User { UserId = 10, FirstName = "יפה", LastName = "כהן", City = "קריית שמונה", State = "KS", Country = "ישראל", Email = "tt@test.com", Password = "12345" },
};

            }
        }

        public List<User> Users
        {
            get { return _sharedViewModel.UsersList; }
        }
    }
}

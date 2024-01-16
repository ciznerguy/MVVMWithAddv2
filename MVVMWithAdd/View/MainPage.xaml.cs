using MVVMWithAdd.Model;
using MVVMWithAdd.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MVVMWithAdd.View
{
    public partial class MainPage : Page
    {
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;


        // טעינת העמוד לתוך המסגרת
        public MainPage(SharedViewModel sharedViewModel)
        {
            InitializeComponent();

            _sharedViewModel = sharedViewModel;
            _userViewModel = new UserViewModel(_sharedViewModel);
            Unloaded += MainPage_Unloaded;
            Loaded += MainPage_Loaded;

            DataContext = _userViewModel;
        }


        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // שימת הפוקוס על המשתמש הראשון ברשימה

            _userViewModel.RefreshUsers();
            if (_sharedViewModel.UsersList.Count > 0)
            {
                UserGrid.SelectedIndex = 0;
                UserGrid.Focus();
            }
        }
        // כפתור הוספה כאשר לוחצים על הכפתור נפתח עמוד חדש
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        // המשתמש הבא ברשימה
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            MoveFocus(true);
        }
        // המשתמש הקודם ברשימה
        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            MoveFocus(false);
        }

        // הזזת הפוקוס אם אפשר להזיז את הפוקוס
        private void MoveFocus(bool isNext)
        {
            int currentIndex = UserGrid.SelectedIndex;

            // חישוב של האינקדס הבא על ידי אופרטור בוליאני
            /*  זהה לכתיבה הבאה
                int newIndex;

                if (isNext) {
                  newIndex = currentIndex + 1;  
                }
                else {
                  newIndex = currentIndex - 1;
}
             * */
            int newIndex = isNext ? currentIndex + 1 : currentIndex - 1;

            // מוודא שלא יוצאים מטווח הרשימה
            if (newIndex < 0)
            {
                newIndex = 0;
            }
            else if (newIndex >= _userViewModel.Users.Count)
            {
                newIndex = _userViewModel.Users.Count - 1;
            }
            // Set focus to the next or previous item
            UserGrid.SelectedIndex = newIndex;
            UserGrid.Focus();
        }
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Code to handle Unloaded event

                // For example, you might want to save or update data when the page is unloaded
                // In this case, we are updating the UsersList in the SharedViewModel

                _sharedViewModel.UsersList = _userViewModel.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during page unload: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

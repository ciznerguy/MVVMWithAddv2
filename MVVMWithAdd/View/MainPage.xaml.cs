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
            int currentIndex = UserGrid.SelectedIndex;//0
           

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
            // העברת הפוקוס למשתמש הבא או הקודם
            UserGrid.SelectedIndex = newIndex;
            UserGrid.Focus();
        }
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // קוד לביצוע פעולות ניקוי כאשר עוזבים את העמוד

                _sharedViewModel.UsersList = _userViewModel.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during page unload: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

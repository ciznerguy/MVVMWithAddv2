// MainWithFrame.xaml.cs

using MVVMWithAdd.ViewModel;
using System.Windows;

namespace MVVMWithAdd.View
{
    /// <summary>
    /// Interaction logic for MainWithFrame.xaml
    /// </summary>
    public partial class MainWithFrame : Window
    {
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;

        public MainWithFrame()
        {
            InitializeComponent();

            // Create an instance of SharedViewModel
            _sharedViewModel = new SharedViewModel();

            // Pass the SharedViewModel instance to UserViewModel
            _userViewModel = new UserViewModel(_sharedViewModel);

            // Set DataContext to _userViewModel
            DataContext = _userViewModel;

            // Navigate to MainPage (passing _sharedViewModel to MainPage constructor)
            mainFrame.Navigate(new LoginPage(_sharedViewModel));
        }

        private void ClickAdd(object sender, RoutedEventArgs e)
        {
            

            if (mainFrame != null)
            {
                mainFrame.Visibility = Visibility.Visible;
                mainFrame.Navigate(new AddUser(_sharedViewModel));
            }
        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            // Close the window
            System.Windows.Application.Current.Shutdown();
            this.Close();
        }
    }
}

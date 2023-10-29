using FamilyTree.BLL.Services;
using System.Windows;
using System.Windows.Input;

namespace FamilyTree.WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            errorLogin.Visibility = Visibility.Hidden;
            if (!checkIsNotEmpty())
            {
                string username = txtUser.Text;
                string password = txtPass.Password;
                UserService userService = new UserService();
                bool isAuthenticated = userService.AuthenticateUser(username, password);
                if (isAuthenticated)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    errorLogin.Visibility = Visibility.Visible;
                }
            }
        }


        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == string.Empty)
            {
                emptyField1.Visibility = Visibility.Visible;
            }
        }

        private void txtPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPass.Password == string.Empty)
            {
                emptyField2.Visibility = Visibility.Visible;
            }
        }
        private bool checkIsNotEmpty()
        {
            return txtUser.Text == string.Empty || txtPass.Password == string.Empty;
        }

        private void txtPass_GotFocus(object sender, RoutedEventArgs e)
        {
            emptyField2.Visibility = Visibility.Hidden;
        }

        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            emptyField1.Visibility = Visibility.Hidden;
        }
    }
}

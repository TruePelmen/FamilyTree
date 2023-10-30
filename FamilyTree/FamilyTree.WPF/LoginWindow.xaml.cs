using FamilyTree.BLL.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FamilyTree.WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DispatcherTimer timer;
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
        private void ShowErrorLoginMessage()
        {
            messageTextBlock.Text = "Неправильний логін або пароль";
            messageTextBlock.Visibility = Visibility.Visible; 
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += (sender, e) =>
            {
                messageTextBlock.Visibility = Visibility.Hidden;
                timer.Stop();
            };
        }
        private void ShowEmptyFieldMessage()
        {
            messageTextBlock.Text = "Заповніть усі обов'язкові поля";
            messageTextBlock.Visibility = Visibility.Visible;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!checkIsNotEmpty())
            {
                UserService userService = new UserService();
                bool isAuthenticated = userService.AuthenticateUser(txtUser.Text, txtPass.Password);
                if (isAuthenticated)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    ShowErrorLoginMessage();
                }
            }
            else
            {
                ShowEmptyFieldMessage();
                if (txtUser.Text == string.Empty) { txtUser.ErrorBorder(); }
                if (txtPass.Password == string.Empty) { txtPass.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0)); }
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
                ShowEmptyFieldMessage();
                txtUser.ErrorBorder();
            }
        }

        private void txtPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPass.Password == string.Empty)
            {
                ShowEmptyFieldMessage();
                txtPass.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }
        private bool checkIsNotEmpty()
        {
            return txtUser.Text == string.Empty || txtPass.Password == string.Empty;
        }

        private void txtPass_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPass.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
            if (txtUser.Text != string.Empty)
            {
                messageTextBlock.Visibility = Visibility.Hidden;
            }

        }
        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUser.NormalBorder();
            if (txtPass.Password != string.Empty)
            {
                messageTextBlock.Visibility = Visibility.Hidden;
            }
        }
    }
}

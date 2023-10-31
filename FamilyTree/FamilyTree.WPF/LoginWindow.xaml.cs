using FamilyTree.BLL.Interfaces;
using FamilyTree.BLL.Interfeces;
using FamilyTree.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FamilyTree.WPF
{
    public partial class LoginWindow : Window
    {
        private DispatcherTimer timer;
        private readonly IUserService _userService;
        public LoginWindow(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
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
            timer.Interval = TimeSpan.FromSeconds(2);
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

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!checkIsNotEmpty())
            {
                bool isAuthenticated = _userService.AuthenticateUser(userTextBox.Text, passwordTextBox.Password);
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
                if (userTextBox.Text == string.Empty) { userTextBox.ErrorBorder(); }
                if (passwordTextBox.Password == string.Empty) { passwordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0)); }
            }
        }


        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(_userService, DependencyContainer.ServiceProvider.GetRequiredService<ITreeService>(),
                DependencyContainer.ServiceProvider.GetRequiredService<IUserTreeService>(), DependencyContainer.ServiceProvider.GetRequiredService<IPersonService>(),
                DependencyContainer.ServiceProvider.GetRequiredService<ITreePersonService>());
            registrationWindow.Show();
            this.Close();
        }

        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (userTextBox.Text == string.Empty)
            {
                ShowEmptyFieldMessage();
                userTextBox.ErrorBorder();
            }
        }

        private void txtPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Password == string.Empty)
            {
                ShowEmptyFieldMessage();
                passwordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }
        private bool checkIsNotEmpty()
        {
            return userTextBox.Text == string.Empty || passwordTextBox.Password == string.Empty;
        }

        private void txtPass_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
            if (userTextBox.Text != string.Empty)
            {
                messageTextBlock.Visibility = Visibility.Hidden;
            }

        }
        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            userTextBox.NormalBorder();
            if (passwordTextBox.Password != string.Empty)
            {
                messageTextBlock.Visibility = Visibility.Hidden;
            }
        }
    }
}

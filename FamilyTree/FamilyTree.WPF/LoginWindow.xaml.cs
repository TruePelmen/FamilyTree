namespace FamilyTree.WPF
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using FamilyTree.BLL.Interfeces;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    /// <summary>
    /// Represents the login window of the application.
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IUserService userService;
        private DispatcherTimer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWindow"/> class.
        /// </summary>
        /// <param name="userService">An instance of the user service used for authentication and user-related operations.</param>
        public LoginWindow(IUserService userService)
        {
            this.InitializeComponent();
            this.userService = userService;
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void BtnMinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowErrorLoginMessage()
        {
            this.messageTextBlock.Text = "Неправильний логін або пароль";
            this.messageTextBlock.Visibility = Visibility.Visible;
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(2);
            this.timer.Start();
            this.timer.Tick += (sender, e) =>
            {
                this.messageTextBlock.Visibility = Visibility.Hidden;
                this.timer.Stop();
            };
        }

        private void ShowEmptyFieldMessage()
        {
            this.messageTextBlock.Text = "Заповніть усі обов'язкові поля";
            this.messageTextBlock.Visibility = Visibility.Visible;
        }

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            if (!this.CheckIsNotEmpty())
            {
                bool isAuthenticated = this.userService.AuthenticateUser(this.userTextBox.Text, this.passwordTextBox.Password);
                if (isAuthenticated)
                {
                    Log.Information("User successfully signed in =)");
                    this.CreateMainWindow(this.userTextBox.Text);
                }
                else
                {
                    this.ShowErrorLoginMessage();
                }
            }
            else
            {
                this.ShowEmptyFieldMessage();
                if (this.userTextBox.Text == string.Empty)
                {
                    this.userTextBox.ErrorBorder();
                }

                if (this.passwordTextBox.Password == string.Empty)
                {
                    this.passwordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
        }

        private void RegisterClick(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registrationWindow = DependencyContainer.ServiceProvider.GetRequiredService<RegistrationWindow>();
            registrationWindow.Show();
            this.Close();
        }

        private void TxtUserLostFocus(object sender, RoutedEventArgs e)
        {
            if (this.userTextBox.Text == string.Empty)
            {
                this.ShowEmptyFieldMessage();
                this.userTextBox.ErrorBorder();
            }
        }

        private void TxtPassLostFocus(object sender, RoutedEventArgs e)
        {
            if (this.passwordTextBox.Password == string.Empty)
            {
                this.ShowEmptyFieldMessage();
                this.passwordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }

        private bool CheckIsNotEmpty()
        {
            return this.userTextBox.Text == string.Empty || this.passwordTextBox.Password == string.Empty;
        }

        private void TxtPassGotFocus(object sender, RoutedEventArgs e)
        {
            this.passwordTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
            if (this.userTextBox.Text != string.Empty)
            {
                this.messageTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void TxtUserGotFocus(object sender, RoutedEventArgs e)
        {
            this.userTextBox.NormalBorder();
            if (this.passwordTextBox.Password != string.Empty)
            {
                this.messageTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void CreateMainWindow(string login)
        {
            MainWindow mainWindow = DependencyContainer.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.UserLogin = login;
            mainWindow.Show();
            this.Close();
        }
    }
}

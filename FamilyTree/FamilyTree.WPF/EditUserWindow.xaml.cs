namespace FamilyTree.WPF
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.BLL.Interfeces;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        private readonly IUserService userService1;
        private readonly IUserService userService;
        private readonly IUserTreeService userTreeService;
        private DispatcherTimer timer;
        private string userLogin = "qwerty";

        public EditUserWindow(IUserService userService, IUserService userService1, IUserTreeService userTreeService)
        {
            this.InitializeComponent();
            this.userService = userService;
            this.userService1 = userService1;
            this.userTreeService = userTreeService;
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
            this.Close();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (!this.CheckIsNotEmpty())
            {
                bool isAuthenticated = this.userService1.AuthenticateUser(this.userLogin, this.firstPassTextBox.Password);
                if (isAuthenticated)
                {
                    this.userService.DeleteUser(this.userLogin);
                    this.userTreeService.DeleteUserTree(this.userLogin);
                    this.Close();
                }
                else
                {
                    this.ShowErrorLoginMessage();
                }
            }
        }

        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            this.CreateLoginWindow();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (!this.CheckIsNotEmpty())
            {
                bool isAuthenticated = this.userService1.AuthenticateUser(this.userLogin, this.firstPassTextBox.Password);
                if (isAuthenticated)
                {
                    this.userService.UpdateUser(this.userLogin, this.passwordTextBox.Password);
                    this.Close();
                }
                else
                {
                    this.ShowErrorLoginMessage();
                }
            }
        }

        private void CreateLoginWindow()
        {
            LoginWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            this.Close();
        }

        private bool CheckIsNotEmpty()
        {
            return this.firstPassTextBox.Password == string.Empty || this.passwordTextBox.Password == string.Empty;
        }

        private void ShowErrorLoginMessage()
        {
            this.messageTextBlock.Text = "Неправильний пароль";
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
    }
}

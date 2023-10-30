using FamilyTree.BLL.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime;
using System;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;
using FamilyTree.WPF.UserControls;

namespace FamilyTree.WPF
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        UserService userService = new UserService();
        public RegistrationWindow()
        {
            InitializeComponent();
            maleOption.IsSelected = true;
            loginTextBox.TextChanged += loginTextBox_TextChanged;
        }
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void MaleOption_Checked(object sender, RoutedEventArgs e)
        {
            maleOption.IsSelected = true;
            femaleOption.IsSelected = false;
        }

        private void FemaleOption_Checked(object sender, RoutedEventArgs e)
        {
            maleOption.IsSelected = false;
            femaleOption.IsSelected = true;
        }
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkIsNotEmpty() && checkPasswordMath() && checkLoginUniqueness())
            {
                userService.AddUser(loginTextBox.Text, passwordBox.Password);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            DateTime? Date = dateOfBirth.SelectedDate;
            string lastName = lastNameTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string gender = maleOption.IsSelected? "Чоловік" : "Жінка";
            if (Date.HasValue)
            {
                string date = Date.Value.ToString("dd/MM/yyyy");
            }
        }
        private void ShowMessage(string message)
        {
            messageField.Text = message;
            messageField.Visibility = Visibility.Visible;
        }

        private void loginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(loginTextBox.Text == string.Empty) 
            { 
                loginTextBox.ErrorBorder(); 
                ShowMessage("Заповніть усі обов'язкові поля"); 
            }
        }

        private void lastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(lastNameTextBox.Text == string.Empty) { lastNameTextBox.ErrorBorder(); ShowMessage("Заповніть усі обов'язкові поля"); }
        }

        private void firstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(firstNameTextBox.Text == string.Empty) {  firstNameTextBox.ErrorBorder(); ShowMessage("Заповніть усі обов'язкові поля"); }
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password == string.Empty) { passwordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0)); ShowMessage("Заповніть усі обов'язкові поля"); }
        }

        private void confirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (confirmPasswordBox.Password == string.Empty) { confirmPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0)); ShowMessage("Заповніть усі обов'язкові поля"); }

        }
        private bool checkIsNotEmpty()
        {
            return !(lastNameTextBox.Text == string.Empty || firstNameTextBox.Text == string.Empty || passwordBox.Password == string.Empty || confirmPasswordBox.Password == string.Empty);
        }
        private bool checkPasswordMath()
        {
            return passwordBox.Password == confirmPasswordBox.Password;
        }

        private void lastNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            lastNameTextBox.NormalBorder();
            if(!checkIsNotEmpty())
            {
                messageField.Visibility = Visibility.Hidden;
            }
        }

        private void firstNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            firstNameTextBox.NormalBorder();
            if (!checkIsNotEmpty())
            {
                messageField.Visibility = Visibility.Hidden;
            }
        }

        private void loginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            loginTextBox.NormalBorder();
            if (!checkIsNotEmpty())
            {
                messageField.Visibility = Visibility.Hidden;
            }
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
            if (!checkIsNotEmpty())
            {
                messageField.Visibility = Visibility.Hidden;
            }
        }

        private void confirmPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            confirmPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
            if (!checkIsNotEmpty())
            {
                messageField.Visibility = Visibility.Hidden;
            }
        }

        private void confirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(!checkPasswordMath())
            {
                confirmPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                ShowMessage("Паролі не співпадають");
            }
            else
            {
                confirmPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
                messageField.Visibility = Visibility.Hidden;
            }
        }
        private bool checkLoginUniqueness()
        {
            return !userService.FindUserByLogin(loginTextBox.Text);
        }
        private void loginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(checkLoginUniqueness())
            {
                loginTextBox.SuccessBorder();
                messageField.Visibility = Visibility.Hidden;
            }
            else
            {
                loginTextBox.ErrorBorder();
                ShowMessage("Користувач з таким логіном вже існує");
            }
        }
    }
}

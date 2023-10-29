using FamilyTree.BLL.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FamilyTree.WPF
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
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
            string lastName = lastNameTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string gender = maleOption.IsSelected? "Чоловік" : "Жінка";
            string dateOfBirth = dateOfBirthTextBox.Text;
            string login = loginTextBox.Text;
            string password = passwordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;
            UserService userService = new UserService();
            userService.AddUser(login, password);

        }
    }
}

namespace FamilyTree.WPF
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.BLL.Interfeces;
    using FamilyTree.BLL.Services;
    using FamilyTree.WPF.UserControls;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    /// <summary>
    /// User registration window.
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private const string ErrorMessageRequiredField = "Заповніть усі обов'язкові поля";
        private const string ErrorMessagePasswordMismatch = "Паролі не співпадають";
        private const string ErrorMessageDuplicateLogin = "Користувач з таким логіном вже існує";
        private readonly IUserService userService;
        private readonly ITreePersonService treePersonService;
        private readonly ITreeService treeService;
        private readonly IUserTreeService userTreeService;
        private readonly IPersonService personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationWindow"/> class.
        /// </summary>
        /// <param name="userService">An instance of the user service for managing user-related operations.</param>
        /// <param name="treeService">An instance of the tree service for managing tree-related operations.</param>
        /// <param name="userTreeService">An instance of the user tree service for managing user-tree relationships.</param>
        /// <param name="personService">An instance of the person service for managing person-related data.</param>
        /// <param name="treePersonService">An instance of the tree person service for managing tree-related person data.</param>
        public RegistrationWindow(IUserService userService, ITreeService treeService, IUserTreeService userTreeService, IPersonService personService, ITreePersonService treePersonService)
        {
            this.InitializeComponent();
            this.InitializeUI();
            this.userService = userService;
            this.treeService = treeService;
            this.userTreeService = userTreeService;
            this.personService = personService;
            this.treePersonService = treePersonService;
        }

        private void InitializeUI()
        {
            this.maleOption.IsSelected = true;
            this.loginTextBox.textBox.TextChanged += this.LoginTextBoxTextChanged;
        }

        private void BtnMinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.CheckLoginUniqueness())
            {
                this.loginTextBox.SuccessBorder();
                this.messageField.Visibility = Visibility.Hidden;
            }
            else
            {
                this.loginTextBox.ErrorBorder();
                this.ShowMessage(ErrorMessageDuplicateLogin);
            }
        }

        private bool CheckLoginUniqueness()
        {
            return !this.userService.FindUserByLogin(this.loginTextBox.Text);
        }

        private void ShowMessage(string message)
        {
            this.messageField.Text = message;
            this.messageField.Visibility = Visibility.Visible;
        }

        private string DetermineGender()
        {
            if (this.maleOption.IsSelected)
            {
                return "male";
            }
            else
            {
                return "female";
            }
        }

        private DateOnly? ParseDate()
        {
            string dateString = this.dateOfBirth.SelectedDate.ToString();
            string dateWithoutTime = dateString.Split(' ')[0];
            return DateOnly.ParseExact(dateWithoutTime, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        private void ContinueButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.CheckFormValidity())
            {
                this.userService.AddUser(this.loginTextBox.Text, this.passwordBox.Password);
                PersonInformation person = new PersonInformation
                {
                    LastName = this.lastNameTextBox.Text,
                    Gender = this.DetermineGender(),
                    MaidenName = null,
                    FirstName = this.firstNameTextBox.Text,
                    OtherNameVariants = null,
                    BirthDate = this.ParseDate(),
                    DeathDate = null,
                };
                int personId = this.personService.AddPerson(person);
                int treeId = this.treeService.AddTree("Дерево " + this.lastNameTextBox.Text, personId);
                this.userTreeService.AddUserTree(this.loginTextBox.Text, treeId, "edit");
                this.treePersonService.AddTreePerson(treeId, personId);
                Log.Information("User was successfully registrated =)");
                MainWindow mainWindow = DependencyContainer.ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.UserLogin = this.loginTextBox.Text;
                mainWindow.Show();
                this.Close();
            }
        }

        private bool CheckFieldEmpty()
        {
            return string.IsNullOrWhiteSpace(this.lastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(this.firstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(this.passwordBox.Password) ||
                string.IsNullOrWhiteSpace(this.confirmPasswordBox.Password) ||
                string.IsNullOrWhiteSpace(this.dateOfBirth.SelectedDate.ToString());
        }

        private bool CheckFormValidity()
        {
            bool isValid = true;

            if (this.CheckFieldEmpty())
            {
                isValid = false;
                this.ShowMessage(ErrorMessageRequiredField);
            }

            if (!string.Equals(this.passwordBox.Password, this.confirmPasswordBox.Password))
            {
                isValid = false;
                this.ShowMessage(ErrorMessagePasswordMismatch);
            }

            if (!this.CheckLoginUniqueness())
            {
                return false;
            }

            return isValid;
        }

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            this.Close();
        }

        private void LastNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTextBoxLostFocus(this.lastNameTextBox);
        }

        private void FirstNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTextBoxLostFocus(this.firstNameTextBox);
        }

        private void LoginTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTextBoxLostFocus(this.loginTextBox);
        }

        private void PasswordBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.HandlePasswordBoxLostFocus(this.passwordBox);
        }

        private void ConfirmPasswordBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.HandlePasswordBoxLostFocus(this.confirmPasswordBox);
            this.passwordBox.PasswordChanged += this.ConfirmPasswordBoxPasswordChanged;
        }

        private void HandleTextBoxLostFocus(MyTextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.ErrorBorder();
                this.ShowMessage(ErrorMessageRequiredField);
            }
        }

        private void HandlePasswordBoxLostFocus(PasswordBox passwordBox)
        {
            if (string.IsNullOrWhiteSpace(this.passwordBox.Password))
            {
                this.passwordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                this.ShowMessage(ErrorMessageRequiredField);
            }
        }

        private void LastNameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTextBoxGotFocus(this.lastNameTextBox);
        }

        private void FirstNameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTextBoxGotFocus(this.firstNameTextBox);
        }

        private void LoginTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTextBoxGotFocus(this.loginTextBox);
        }

        private void PasswordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            this.HandlePasswordBoxGotFocus(this.passwordBox);
        }

        private void ConfirmPasswordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            this.HandlePasswordBoxGotFocus(this.confirmPasswordBox);
        }

        private void HandleTextBoxGotFocus(MyTextBox textBox)
        {
            textBox.NormalBorder();
            if (!this.CheckFieldEmpty())
            {
                this.messageField.Visibility = Visibility.Hidden;
            }
        }

        private void HandlePasswordBoxGotFocus(PasswordBox passwordBox)
        {
            this.passwordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
            if (!this.CheckFieldEmpty())
            {
                this.messageField.Visibility = Visibility.Hidden;
            }
        }

        private void ConfirmPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.Equals(this.passwordBox.Password, this.confirmPasswordBox.Password))
            {
                this.ShowMessage(ErrorMessagePasswordMismatch);
            }
            else
            {
                this.messageField.Visibility = Visibility.Hidden;
            }
        }

        private void DateOfBirthLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.dateOfBirth.SelectedDate.ToString()))
            {
                this.dateOfBirth.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                this.ShowMessage(ErrorMessageRequiredField);
            }
        }

        private void DateOfBirthGotFocus(object sender, RoutedEventArgs e)
        {
            this.dateOfBirth.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
            if (!this.CheckFieldEmpty())
            {
                this.messageField.Visibility = Visibility.Hidden;
            }
        }
    }
}

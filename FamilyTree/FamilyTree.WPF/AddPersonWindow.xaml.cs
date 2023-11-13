namespace FamilyTree.WPF
{
    using System.Globalization;
    using System;
    using System.Windows;
    using FamilyTree.BLL.Interfaces;
    using System.Windows.Controls;
    using System.Windows.Media;
    using MaterialDesignThemes.Wpf;
    using System.Windows.Input;
    using Microsoft.Win32;
    using static System.Net.Mime.MediaTypeNames;
    using System.Windows.Media.Imaging;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for AddPersonWindow.xaml.
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        private readonly ITreePersonService treePersonService;
        private readonly ITreeService treeService;
        private readonly IPersonService personService;

        public AddPersonWindow()
        {
            InitializeComponent();
            this.treePersonService = treePersonService;
            this.treeService = treeService;
            this.personService = personService;
        }

        private void AddNewPersonButtonClick(object sender, RoutedEventArgs e)
        {
            this.CheckFieldEmpty();
            int personId = this.personService.AddPerson(true, this.addLastNameTextBox.Text, this.DetermineGender(), this.addMaidenNameTextBox.Text, 
                this.addFirstNameTextBox.Text, this.addOtherNameTextBox.Text, this.ParseDate(this.dateOfBirth.SelectedDate.ToString()), this.ParseDate(this.dateOfDeath.SelectedDate.ToString()));
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

        private void FemaleOption_Checked(object sender, RoutedEventArgs e)
        {
            this.HandleGenderSelection("female");
        }

        private void MaleOption_Checked(object sender, RoutedEventArgs e)
        {
            this.HandleGenderSelection("male");
        }

        private void HandleGenderSelection(string gender)
        {
            if (gender == "female")
            {
                this.addMaidenNameTextBox.Visibility = Visibility.Visible;
            }
            else if (gender == "male")
            {
                this.addMaidenNameTextBox.Visibility = Visibility.Collapsed;
            }

        }

        private DateOnly? ParseDate(string dateString)
        {
            string dateWithoutTime = dateString.Split(' ')[0];
            return DateOnly.ParseExact(dateWithoutTime, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        private void CheckFieldEmpty()
        {
            if (string.IsNullOrWhiteSpace(this.addLastNameTextBox.Text))
            {
                this.addLastNameTextBox.Focus();
            }
            else if (!this.maleOption.IsSelected || !this.femaleOption.IsSelected)
            {
                this.genderWarning.Visibility = Visibility.Visible;
            }
        }

        private void ChangeImage(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.InitialDirectory = "D:\\Lab\\enginLab\\FamilyTree\\FamilyTree\\FamilyTree.WPF\\Images";

            if (openFileDialog.ShowDialog() == true)
            {
                string newImagePath = openFileDialog.FileName;
                this.myImage.Source = new BitmapImage(new Uri(newImagePath));
            }
        }

        private void BtnMinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void LastNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTextBoxLostFocus(this.addLastNameTextBox);
        }

        private void LastNameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            HintAssist.SetHint(this.addLastNameTextBox, "Прізвище");
        }

        private void HandleTextBoxLostFocus(TextBox textBox)
        {
            var testBoxHit = HintAssist.GetHint(textBox);
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                HintAssist.SetHint(textBox, $"{testBoxHit} (Обов'язкове поле)");
            }
            else
            {
                textBox.BorderBrush = SystemColors.ControlDarkDarkBrush;
                HintAssist.SetHint(textBox, $"{testBoxHit}");
            }
        }

        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            this.addLastNameTextBox.Text = null;
            this.addFirstNameTextBox.Text = null;
            this.addMaidenNameTextBox.Text = null;
            this.addOtherNameTextBox.Text = null;
            this.dateOfBirth.Text = null;
            this.dateOfDeath.Text = null;
            this.maleOption.IsSelected = false;
            this.femaleOption.IsSelected = false;
        }

        private void addEventButtonClick(object sender, RoutedEventArgs e)
        {
            AddEvent addEventWindow = DependencyContainer.ServiceProvider.GetRequiredService<AddEvent>();
            addEventWindow.Show();
            this.Close();
        }
    }
}

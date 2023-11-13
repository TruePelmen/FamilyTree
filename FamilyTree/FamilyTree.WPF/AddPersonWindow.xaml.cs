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
    using FamilyTree.BLL;
    using FamilyTree.DAL.Models;
    using FamilyTree.WPF.UserControls;

    /// <summary>
    /// Interaction logic for AddPersonWindow.xaml.
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        private readonly ITreePersonService treePersonService;
        private readonly ITreeService treeService;
        private readonly IPersonService personService;

        public AddPersonWindow(ITreePersonService treePersonService, ITreeService treeService, IPersonService personService)
        {
            this.InitializeComponent();
            this.treePersonService = treePersonService;
            this.treeService = treeService;
            this.personService = personService;
        }

        private void AddNewPersonButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.CheckFieldEmpty())
            {
                PersonInformation person = new PersonInformation
                {
                    LastName = this.LastNameTextBox.Text,
                    FirstName = this.FirstNameTextBox.Text,
                    MaidenName = this.MaidenNameTextBox.Text,
                    BirthDate = this.ParseDate(this.dateOfBirth),
                    DeathDate = this.ParseDate(this.dateOfDeath),
                    Gender = this.DetermineGender(),
                };
                int personId = this.personService.AddPerson(person);
                int treeId = this.treeService.AddTree("Дерево " + this.LastNameTextBox.Text, personId);
                this.treePersonService.AddTreePerson(treeId, personId);
            }
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

        private DateOnly? ParseDate(DatePicker date)
        {
            if (!string.IsNullOrWhiteSpace(date.SelectedDate.ToString())) 
            {
                string dateString = date.SelectedDate.ToString();
                string dateWithoutTime = dateString.Split(' ')[0];
                return DateOnly.ParseExact(dateWithoutTime, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            return null;
        }

        private bool CheckFieldEmpty()
        {
            if (string.IsNullOrWhiteSpace(this.LastNameTextBox.Text))
            {
                this.LastNameTextBox.Focus();
                return false;
            }
            else if (!this.maleOption.IsSelected && !this.femaleOption.IsSelected)
            {
                this.genderWarning.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                return true;
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
            this.HandleTextBoxLostFocus(this.LastNameTextBox);
        }

        private void LastNameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            HintAssist.SetHint(this.LastNameTextBox, "Прізвище");
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

        private void MaidenNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (!this.femaleOption.IsSelected)
            {
                this.MaidenNameTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }

        private void MaidenNameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (this.femaleOption.IsSelected)
            {
                this.MaidenNameTextBox.BorderBrush = SystemColors.ControlDarkDarkBrush;
                HintAssist.SetHint(this.MaidenNameTextBox, "Жіноче прізвище");
                this.MaidenNameTextBox.IsReadOnly = false;
            }
        }

        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            this.LastNameTextBox.Text = null;
            this.FirstNameTextBox.Text = null;
            this.MaidenNameTextBox.Text = null;
            this.OtherNameTextBox.Text = null;
            this.dateOfBirth.Text = null;
            this.dateOfDeath.Text = null;
            this.maleOption.IsSelected = false;
            this.femaleOption.IsSelected = false;
            this.MaidenNameTextBox.IsReadOnly = true;
            this.genderWarning.Visibility= Visibility.Collapsed;
        }

        private void AddEventButtonClick(object sender, RoutedEventArgs e)
        {
            AddEvent addEventWindow = DependencyContainer.ServiceProvider.GetRequiredService<AddEvent>();
            addEventWindow.Show();
            this.Close();
        }
    }
}

namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using FamilyTree.DAL.Models;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for FocusPersonCard.xaml.
    /// </summary>
    public partial class FocusPersonCard : UserControl, IPersonCard
    {
        private new const int MaxWidth = 275;
        private bool isEmpty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FocusPersonCard"/> class.
        /// </summary>
        public FocusPersonCard()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Event that occurs when a person is deleted.
        /// </summary>
        public event EventHandler<int> DeletePerson;

        public event EventHandler<int> PersonAddedFocus;

        /// <summary>
        /// Gets or sets the ID of the person.
        /// </summary>
        public int IdPerson { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the person card is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.isEmpty;
            }

            set
            {
                this.isEmpty = value;
                this.CheckIsEmptyForm();
            }
        }

        /// <summary>
        /// Sets the default photo based on the gender.
        /// </summary>
        /// <param name="gender">The gender of the person (e.g., "male" or "female").</param>
        public void SetDefaultPhoto(string gender)
        {
            if (gender == "male")
            {
                this.ChangeMainPhoto("C:/Users/olesy/OneDrive/Документи/GitHub/ProgramEngineeringProject-/FamilyTree/FamilyTree.WPF/Images/man.png");
            }
            else
            {
                this.ChangeMainPhoto("C:/Users/olesy/OneDrive/Документи/GitHub/ProgramEngineeringProject-/FamilyTree/FamilyTree.WPF/Images/woman.png");
            }
        }

        /// <summary>
        /// Renews the information on the person card.
        /// </summary>
        /// <param name="person">The information about the person to display on the card.</param>
        public void RenewPersonCard(PersonInformation person)
        {
            this.IsEmpty = person.IsEmptyPerson;
            if (!this.isEmpty)
            {
                this.RenewInformation(person);
            }
        }

        private void CheckIsEmptyForm()
        {
            if (this.isEmpty)
            {
                this.mainPanel.Visibility = Visibility.Hidden;
                this.emptyPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.mainPanel.Visibility = Visibility.Visible;
                this.emptyPanel.Visibility = Visibility.Hidden;
            }
        }

        private void AdjustFontSizeToFit(TextBlock textBlock, double maxWidth)
        {
            double pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
            var formattedText = new FormattedText(
                textBlock.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                TextFormattingMode.Display,
                pixelsPerDip);

            while (formattedText.Width > maxWidth)
            {
                textBlock.FontSize -= 1;
                formattedText.SetFontSize(textBlock.FontSize);
            }
        }

        private void ChangeName(string name)
        {
            this.nameTextBlock.Text = name;
            this.AdjustFontSizeToFit(this.nameTextBlock, MaxWidth);
        }

        private void ChangeDateOfBirth(DateOnly dateOfBirth)
        {
            this.yearOfLife.Text = $"({dateOfBirth.Year})";
            this.dateOfDeathLabel.Visibility = Visibility.Hidden;
            this.placeOfDeathLabel.Visibility = Visibility.Hidden;
            this.dateOfBirthValue.Text = dateOfBirth.ToString("dd.MM.yyyy");
        }

        private void ChangeDateOfDeath(DateOnly dateOfDeath)
        {
            this.yearOfLife.Text = $"( - {dateOfDeath.Year})";
            this.dateOfDeathLabel.Visibility = Visibility.Visible;
            this.dateOfDeathValue.Text = dateOfDeath.ToString("dd.MM.yyyy");
        }

        private void ChangeYearOfLife(DateOnly dateOfBirth, DateOnly dateOfDeath)
        {
            this.yearOfLife.Text = $"({dateOfBirth.Year} - {dateOfDeath.Year})";
            this.dateOfDeathLabel.Visibility = Visibility.Visible;
            this.placeOfDeathLabel.Visibility = Visibility.Visible;
            this.dateOfDeathValue.Visibility = Visibility.Visible;
            this.placeOfDeathValue.Visibility = Visibility.Visible;
            this.dateOfBirthValue.Text = dateOfBirth.ToString("dd.MM.yyyy");
            this.dateOfDeathValue.Text = dateOfDeath.ToString("dd.MM.yyyy");
        }

        private void ChangePlaceOfBirth(string placeOfBirth)
        {
            this.placeOfBirthLabel.Visibility = Visibility.Visible;
            this.placeOfBirthValue.Visibility = Visibility.Visible;
            this.placeOfBirthValue.Text = placeOfBirth;
        }

        private void ChangePlaceOfDeath(string placeOfDeath)
        {
            this.placeOfDeathLabel.Visibility = Visibility.Visible;
            this.placeOfDeathValue.Visibility = Visibility.Visible;
            this.placeOfDeathValue.Text = placeOfDeath;
        }

        private void ChangeMainPhoto(string mainPhotoPath)
        {
            this.personImage.Source = new BitmapImage(new Uri(mainPhotoPath));
        }

        private void RenewInformation(PersonInformation person)
        {
            this.ClearLabels();
            this.ChangeName(person.LastName + " " + person.FirstName);
            if (person.BirthDate != null && person.DeathDate != null)
            {
                this.ChangeYearOfLife((DateOnly)person.BirthDate, (DateOnly)person.DeathDate);
            }
            else if (person.BirthDate != null)
            {
                this.ChangeDateOfBirth((DateOnly)person.BirthDate);
            }
            else if (person.DeathDate != null)
            {
                this.ChangeDateOfDeath((DateOnly)person.DeathDate);
            }

            if (person.MainPhoto != null)
            {
                this.ChangeMainPhoto(person.MainPhoto);
            }
            else
            {
                this.SetDefaultPhoto(person.Gender);
            }

            if (person.BirthPlace != null && person.BirthPlace != string.Empty)
            {
                this.ChangePlaceOfBirth(person.BirthPlace);
            }

            if (person.DeathPlace != null && person.DeathPlace != string.Empty)
            {
                this.ChangePlaceOfDeath(person.DeathPlace);
            }
        }

        private void ClearLabels()
        {
            this.yearOfLife.Text = string.Empty;
            this.dateOfBirthValue.Text = "Невідомо";
            this.dateOfDeathValue.Text = "Невідомо";
            this.placeOfBirthValue.Text = "Невідомо";
            this.placeOfDeathValue.Text = "Невідомо";
            this.placeOfDeathLabel.Visibility = Visibility.Hidden;
            this.dateOfDeathLabel.Visibility = Visibility.Hidden;
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Ви впевнені, що бажаєте видалити особу: {this.nameTextBlock.Text}?", "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int id = this.IdPerson;
                this.DeletePerson.Invoke(this, this.IdPerson);
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = DependencyContainer.ServiceProvider.GetRequiredService<AddPersonWindow>();
            addPersonWindow.AddNewPerson += (s, args) =>
            {
                this.IdPerson = addPersonWindow.IdNewPerson;
                this.PersonAddedFocus?.Invoke(this, this.IdPerson);
            };

            addPersonWindow.Show();
        }

        private void ProfileButtonClick(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = DependencyContainer.ServiceProvider.GetRequiredService<ProfileWindow>();
            profileWindow.Id = this.IdPerson;
            profileWindow.ShowDialog();
        }
    }
}

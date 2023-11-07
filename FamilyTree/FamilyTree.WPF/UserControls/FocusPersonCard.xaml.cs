// <copyright file="FocusPersonCard.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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

        public event EventHandler<int> DeletePerson;
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

        public bool IsPrimaryPerson { get; set; }

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

        [Obsolete]
        /// <summary>
        /// Renews the information on the person card.
        /// </summary>
        /// <param name="person">The information about the person to display on the card.</param>
        public void RenewPersonCard(PersonCardInformation person)
        {
            this.IsEmpty = person.IsEmptyPerson;
            if (!this.isEmpty)
            {
                this.RenewInformation(person);
                this.IsPrimaryPerson = person.Person.PrimaryPerson;
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

        [Obsolete]
        private void AdjustFontSizeToFit(TextBlock textBlock, double maxWidth)
        {
            var formattedText = new FormattedText(
                textBlock.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                TextFormattingMode.Display);

            while (formattedText.Width > maxWidth)
            {
                textBlock.FontSize -= 1;
                formattedText.SetFontSize(textBlock.FontSize);
            }
        }

        [Obsolete]
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
            this.yearOfLife.Text = $"({dateOfDeath.Year})";
            this.dateOfDeathLabel.Visibility = Visibility.Visible;
            this.dateOfDeathValue.Text = dateOfDeath.ToString("dd.MM.yyyy");
        }

        private void ChangeYearOfLife(DateOnly dateOfBirth, DateOnly dateOfDeath)
        {
            this.yearOfLife.Text = $"({dateOfBirth.Year} - {dateOfDeath.Year})";
            this.dateOfDeathLabel.Visibility = Visibility.Visible;
            this.placeOfDeathLabel.Visibility = Visibility.Visible;
            this.dateOfBirthValue.Text = dateOfBirth.ToString("dd.MM.yyyy");
            this.dateOfDeathValue.Text = dateOfDeath.ToString("dd.MM.yyyy");
        }

        private void ChangePlaceOfBirth(string placeOfBirth)
        {
            this.placeOfBirthValue.Text = placeOfBirth;
        }

        private void ChangePlaceOfDeath(string placeOfDeath)
        {
            this.placeOfDeathValue.Visibility = Visibility.Visible;
            this.placeOfDeathValue.Text = placeOfDeath;
        }

        private void ChangeMainPhoto(string mainPhotoPath)
        {
            this.personImage.Source = new BitmapImage(new Uri(mainPhotoPath));
        }

        [Obsolete]
        private void RenewInformation(PersonCardInformation person)
        {
            this.ChangeName(person.Person.LastName + " " + person.Person.FirstName);
            if (person.Person.BirthDate != null && person.Person.DeathDate != null)
            {
                this.ChangeYearOfLife((DateOnly)person.Person.BirthDate, (DateOnly)person.Person.DeathDate);
            }
            else if (person.Person.BirthDate != null)
            {
                this.ChangeDateOfBirth((DateOnly)person.Person.BirthDate);
            }
            else if (person.Person.DeathDate != null)
            {
                 this.ChangeDateOfDeath((DateOnly)person.Person.DeathDate);
            }

            if (person.MainPhoto != null)
            {
                this.ChangeMainPhoto(person.MainPhoto);
            }
            else
            {
                this.SetDefaultPhoto(person.Person.Gender);
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

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.IsPrimaryPerson)
            {
                MessageBox.Show("Неможливо видалити головну особу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Ви впевнені, що бажаєте видалити особу: {this.nameTextBlock.Text}?", "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = this.IdPerson;
                    this.DeletePerson.Invoke(this, this.IdPerson);
                    this.IsEmpty = true;
                }
            }
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = DependencyContainer.ServiceProvider.GetRequiredService<EditWindow>();
            editWindow.Id = this.IdPerson;
            editWindow.Show();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.ShowDialog();
        }

        private void NameTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Id = this.IdPerson;
            profileWindow.ShowDialog();
        }
    }
}

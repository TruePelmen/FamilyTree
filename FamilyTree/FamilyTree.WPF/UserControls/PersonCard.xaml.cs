// <copyright file="PersonCard.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;

    /// <summary>
    /// Represents a user control for displaying information about a person in the FamilyTree application.
    /// </summary>
    public partial class PersonCard : UserControl, IPersonCard
    {
        private bool isEmpty;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonCard"/> class.
        /// </summary>
        public PersonCard()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the unique identifier of the person associated with this card.
        /// </summary>
        public int IdPerson { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the card is empty, i.e., does not display person information.
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
        /// Sets the default photo for the card based on the person's gender.
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
        /// Checks whether the card is empty and adjusts its visibility accordingly.
        /// </summary>
        public void CheckIsEmptyForm()
        {
            if (this.IsEmpty)
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

        /// <summary>
        /// Changes the name displayed on the card.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        public void ChangeName(string name)
        {
            this.nameTextBlock.Text = name;
        }

        /// <summary>
        /// Changes the date of birth displayed on the card.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth of the person.</param>
        public void ChangeDateOfBirth(DateOnly dateOfBirth)
        {
            this.yearOfLife.Text = $"({dateOfBirth.Year})";
        }

        /// <summary>
        /// Changes the date of death displayed on the card.
        /// </summary>
        /// <param name="dateOfDeath">The date of death of the person.</param>
        public void ChangeDateOfDeath(DateOnly dateOfDeath)
        {
            this.yearOfLife.Text = $"( - {dateOfDeath.Year})";
        }

        /// <summary>
        /// Changes the year of life displayed on the card based on date of birth and date of death.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth of the person.</param>
        /// <param name="dateOfDeath">The date of death of the person.</param>
        public void ChangeYearOfLife(DateOnly dateOfBirth, DateOnly dateOfDeath)
        {
            this.yearOfLife.Text = $"({dateOfBirth.Year} - {dateOfDeath.Year})";
        }

        /// <summary>
        /// Changes the main photo displayed on the card.
        /// </summary>
        /// <param name="mainPhotoPath">The path to the main photo of the person.</param>
        public void ChangeMainPhoto(string mainPhotoPath)
        {
            this.personImage.Source = new BitmapImage(new Uri(mainPhotoPath));
        }

        /// <summary>
        /// Renews the information displayed on the card using data from a <see cref="PersonCardInformation"/> object.
        /// </summary>
        /// <param name="person">A <see cref="PersonCardInformation"/> object containing person information.</param>
        public void RenewPersonCard(PersonCardInformation person)
        {
            this.IsEmpty = person.IsEmptyPerson;
            if (!this.IsEmpty)
            {
                this.RenewInformation(person);
            }
        }

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
        }

        private void NameTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Id = this.IdPerson;
            profileWindow.ShowDialog();
        }

        private void AddPersonButtonClick(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.ShowDialog();
        }
    }
}

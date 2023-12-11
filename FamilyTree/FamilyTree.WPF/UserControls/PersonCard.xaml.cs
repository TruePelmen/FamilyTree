namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonCard : UserControl, IPersonCard
    {
        private bool isEmpty;

        public PersonCard()
        {
            this.InitializeComponent();
        }

        public int IdPerson { get; set; }

        public event EventHandler<int> PersonAdded;

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

        public void ChangeName(string name)
        {
            this.nameTextBlock.Text = name;
        }

        public void ChangeDateOfBirth(DateOnly dateOfBirth)
        {
            this.yearOfLife.Text = $"({dateOfBirth.Year})";
        }

        public void ChangeDateOfDeath(DateOnly dateOfDeath)
        {
            this.yearOfLife.Text = $"( - {dateOfDeath.Year})";
        }

        public void ChangeYearOfLife(DateOnly dateOfBirth, DateOnly dateOfDeath)
        {
            this.yearOfLife.Text = $"({dateOfBirth.Year} - {dateOfDeath.Year})";
        }

        public void ChangeMainPhoto(string mainPhotoPath)
        {
            this.personImage.Source = new BitmapImage(new Uri(mainPhotoPath));
        }

        public void RenewPersonCard(PersonInformation person)
        {
            this.IsEmpty = person.IsEmptyPerson;
            if (!this.IsEmpty)
            {
                this.RenewInformation(person);
            }
        }

        private void RenewInformation(PersonInformation person)
        {
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
        }

        private void NameTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProfileWindow profileWindow = DependencyContainer.ServiceProvider.GetRequiredService<ProfileWindow>();
            profileWindow.Id = this.IdPerson;
            profileWindow.ShowDialog();
        }

        private void AddPersonButtonClick(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = DependencyContainer.ServiceProvider.GetRequiredService<AddPersonWindow>();
            addPersonWindow.Closed += (s, args) =>
            {
                this.IdPerson = addPersonWindow.IdNewPerson;
                this.PersonAdded?.Invoke(this, this.IdPerson);
            };

            addPersonWindow.Show();
        }
    }
}

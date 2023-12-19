namespace FamilyTree.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.WPF.UserControls;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for ProfileWindow.xaml.
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly IPersonService personService;
        private readonly IRelationshipService relationshipService;
        private readonly IMediaEventService mediaEventService;
        private readonly IMediaPersonService mediaPersonService;
        private readonly IEventService eventService;
        private int id;
        private PersonInformation personInformation;
        private string gender;
        private string spouseGender;
        private PersonInformation father;
        private PersonInformation mother;
        private PersonInformation spouse;
        private List<PersonInformation> children;
        private List<PersonInformation> siblings;
        private List<Photo> photos;
        private List<EventInformation> events;

        public ProfileWindow(IPersonService personService, IRelationshipService relationshipService, IMediaEventService mediaEventService, IMediaPersonService mediaPersonService, IEventService eventService)
        {
            this.InitializeComponent();
            this.relationshipService = relationshipService;
            this.personService = personService;
            this.children = new List<PersonInformation>();
            this.siblings = new List<PersonInformation>();
            this.events = new List<EventInformation>();
            this.photos = new List<Photo>();
            this.mediaEventService = mediaEventService;
            this.mediaPersonService = mediaPersonService;
            this.eventService = eventService;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.GetPerson();
                this.GetShortPersonInformation();
                this.GetNameInformation();
                this.GetRelativeInformation();
                this.FillCloseRelativePanel();
                this.GetAllPhoto();
                this.ShowPhotos();
                this.ShowPersonEvents();
            }
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

        private void GetPerson()
        {
            this.personInformation = this.personService.GetPersonById(this.Id);
            if (this.personInformation.Gender == "male")
            {
                this.gender = "Його";
                this.spouseGender = "дружина";
            }
            else
            {
                this.gender = "Її";
                this.spouseGender = "чоловік";
            }
        }

        private void GetShortPersonInformation()
        {
            this.nameTextBlock.Text = this.personInformation.FullName;
            if (this.personInformation.BirthDate != null)
            {
                this.birthDateTextBlock.Text = this.personInformation.BirthDate?.ToString("dd.MM.yyyy");
            }

            if (this.personInformation.BirthPlace != null)
            {
                this.birthPlaceTextBlock.Text = this.personInformation.BirthPlace;
            }

            if (this.personInformation.DeathDate != null)
            {
                this.deathTextBlock.Visibility = Visibility.Visible;
                this.deathDateTextBlock.Text = this.personInformation.DeathDate?.ToString("dd.MM.yyyy");
            }

            if (this.personInformation.DeathPlace != null)
            {
                this.deathTextBlock.Visibility = Visibility.Visible;
                this.deathPlaceTextBlock.Text = this.personInformation.DeathPlace;
            }

            if (this.personInformation.MainPhoto != null)
            {
                this.photo.Source = new BitmapImage(new Uri(this.personInformation.MainPhoto));
            }
            else
            {
                if (this.personInformation.Gender == "male")
                {
                    this.photo.Source = new BitmapImage(new Uri("C:\\University\\SoftwareEngineering\\18.12.2023\\FamilyTree\\FamilyTree.WPF\\Images\\man.png"));
                }
                else
                {
                    this.photo.Source = new BitmapImage(new Uri("C:\\University\\SoftwareEngineering\\18.12.2023\\FamilyTree\\FamilyTree.WPF\\Images\\woman.png"));
                }
            }
        }

        private void GetNameInformation()
        {
            this.lastNameTextBox.Text = this.personInformation.LastName;
            if (this.personInformation.FirstName != null)
            {
                this.firstNameTextBox.Text = this.personInformation.FirstName;
            }

            if (this.personInformation.MaidenName != null)
            {
                this.maidenTextBlock.Visibility = Visibility.Visible;
                this.maidenNameTextBox.Text = this.personInformation.MaidenName;
            }

            if (this.personInformation.OtherNameVariants != null)
            {
                this.otherName.Visibility = Visibility.Visible;
                this.otherNameTextBox.Text = this.personInformation.OtherNameVariants;
            }
        }

        private void GetRelativeInformation()
        {
            int fatherId = this.relationshipService.GetFatherIdByPersonId(this.Id);
            int motherId = this.relationshipService.GetMotherIdByPersonId(this.Id);

            this.spouse = this.personService.GetPersonById(this.relationshipService.GetSpouseIdByPersonId(this.Id));
            this.father = this.personService.GetPersonById(fatherId);
            this.mother = this.personService.GetPersonById(motherId);

            var children = this.relationshipService.GetChildrenIdByPersonId(this.Id);
            foreach (var child in children)
            {
                this.children.Add(this.personService.GetPersonById(child));
            }

            var childrenOfFather = this.relationshipService.GetChildrenIdByPersonId(fatherId);
            var childrenOfMother = this.relationshipService.GetChildrenIdByPersonId(motherId);

            var siblings = new HashSet<PersonInformation>();

            // Додаємо братів і сестер в колекцію siblings, перевіряючи, чи вони вже є у списку
            foreach (var childId in childrenOfFather.Concat(childrenOfMother))
            {
                var sibling = this.personService.GetPersonById(childId);
                if (sibling.Id != this.Id && !siblings.Any(s => s.Id == sibling.Id))
                {
                    siblings.Add(sibling);
                }
            }

            this.siblings = siblings.ToList();
        }

        private void FillCloseRelativePanel()
        {
            if (!this.spouse.IsEmptyPerson)
            {
                RelativeCard spouseCard = new RelativeCard();
                spouseCard.TransferToAnotherPerson += this.TransferToAnotherPeson;
                spouseCard.RenewCardInformation(this.spouse, this.gender + " " + this.spouseGender);
                this.closeRelativesPanel.Children.Add(spouseCard);
            }

            if (!this.father.IsEmptyPerson)
            {
                RelativeCard fatherCard = new RelativeCard();
                fatherCard.TransferToAnotherPerson += this.TransferToAnotherPeson;
                fatherCard.RenewCardInformation(this.father, this.gender + " батько");
                this.closeRelativesPanel.Children.Add(fatherCard);
            }

            if (!this.mother.IsEmptyPerson)
            {
                RelativeCard motherCard = new RelativeCard();
                motherCard.TransferToAnotherPerson += this.TransferToAnotherPeson;
                motherCard.RenewCardInformation(this.mother, this.gender + " мати");
                this.closeRelativesPanel.Children.Add(motherCard);
            }

            if (this.children.Count > 0)
            {
                foreach (var child in this.children)
                {
                    string childGender = string.Empty;
                    if (child.Gender == "male")
                    {
                        childGender = "син";
                    }
                    else
                    {
                        childGender = "дочка";
                    }

                    RelativeCard childCard = new RelativeCard();
                    childCard.TransferToAnotherPerson += this.TransferToAnotherPeson;
                    childCard.RenewCardInformation(child, this.gender + " " + childGender);
                    this.closeRelativesPanel.Children.Add(childCard);
                }
            }

            if (this.siblings.Count > 0)
            {
                foreach (var sibling in this.siblings)
                {
                    string siblingGender = string.Empty;
                    if (sibling.Gender == "male")
                    {
                        siblingGender = "брат";
                    }
                    else
                    {
                        siblingGender = "сестра";
                    }

                    RelativeCard siblingCard = new RelativeCard();
                    siblingCard.TransferToAnotherPerson += this.TransferToAnotherPeson;
                    siblingCard.RenewCardInformation(sibling, this.gender + " " + siblingGender);
                    this.closeRelativesPanel.Children.Add(siblingCard);
                }
            }
        }

        private void TransferToAnotherPeson(object sender, int personId)
        {
            ProfileWindow profileWindow = DependencyContainer.ServiceProvider.GetRequiredService<ProfileWindow>();
            profileWindow.Id = personId;
            profileWindow.Show();
            this.Close();
        }

        private void GetAllPhoto()
        {
            this.photos.AddRange(this.mediaPersonService.GetAllMediaByPersonId(this.Id));
            this.photos.AddRange(this.mediaEventService.GetAllPhotosForPerson(this.Id));
        }

        private void ShowPhotos()
        {
            foreach (var photo in this.photos)
            {
                var addPhoto = this.addPhoto;
                this.photoPanel.Children.Remove(addPhoto);
                PhotoCard photoCard = new PhotoCard();
                photoCard.RenewCard(photo);
                photoCard.OpenPhotoWindow += this.PhotoCardOpenPhotoWindow;
                this.photoPanel.Children.Add(photoCard);
                this.photoPanel.Children.Add(addPhoto);
            }
        }

        private void TransferToAnotherPerson(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhotoCardOpenPhotoWindow(object sender, int id)
        {
            PhotoWindow photoWindow = DependencyContainer.ServiceProvider.GetRequiredService<PhotoWindow>();
            photoWindow.Id = id;
            photoWindow.TransferToAnotherPerson += this.TransferToAnotherPerson;
            photoWindow.DeletePhoto += this.PhotoWindowDeletePhoto;
            photoWindow.Show();
        }

        private void PhotoWindowDeletePhoto(object sender, EventArgs e)
        {
            ProfileWindow profileWindow = DependencyContainer.ServiceProvider.GetRequiredService<ProfileWindow>();
            profileWindow.Id = this.Id;
            profileWindow.Show();
            this.Close();
        }

        private void GetAllEvents()
        {
            this.events.Clear();
            this.events.AddRange(this.eventService.GetEventsByPersonId(this.Id));
            var spouseEvents = this.eventService.GetAllEventsByPersonIdAndEventType(this.relationshipService.GetSpouseIdByPersonId(this.Id), "death");
            foreach (var spouseEvent in spouseEvents)
            {
                spouseEvent.FullEventType += " " + this.spouseGender + " " + this.spouse.FullName;
                spouseEvent.Age = this.personInformation.CurrentAge(spouseEvent.EventDate?.Year);
                this.events.Add(spouseEvent);
            }

            foreach (var child in this.children)
            {
                string childGender = string.Empty;
                if (child.Gender == "male")
                {
                    childGender = " сина";
                }
                else
                {
                    childGender = " дочки";
                }

                var childEvents = this.eventService.GetImportantEventsByPersonId(child.Id);
                foreach (var childEvent in childEvents)
                {
                    childEvent.FullEventType += childGender + " " + child.FullName;
                    childEvent.Age = this.personInformation.CurrentAge(childEvent.EventDate?.Year);
                    this.events.Add(childEvent);
                }
            }

            this.events = this.events.OrderBy(e => e.EventDate).ToList();
            if (this.personInformation.DeathDate != null && this.events.Count != 0)
            {
                this.events = this.events.Where(e => e.EventDate <= this.personInformation.DeathDate).OrderBy(e => e.EventDate).ToList();
            }
        }

        private void ShowPersonEvents()
        {
            this.GetAllEvents();
            foreach (var personEvent in this.events)
            {
                var addEvent = this.addEventRecord;
                this.eventsPanel.Children.Remove(addEvent);
                EventRecord eventRecord = new EventRecord(personEvent);
                eventRecord.DeleteEvent += this.EventRecordDeleteEvent;
                this.eventsPanel.Children.Add(eventRecord);
                Separator separator = new Separator();
                separator.Style = (Style)this.FindResource("MaterialDesignDarkSeparator");
                this.eventsPanel.Children.Add(separator);
                this.eventsPanel.Children.Add(addEvent);
            }
        }

        private void EventRecordDeleteEvent(object sender, EventArgs e)
        {
            this.eventService.DeleteEvent((sender as EventRecord).Id);
            var addEvent = this.addEventRecord;
            this.eventsPanel.Children.Clear();
            this.eventsPanel.Children.Add(addEvent);
            this.ShowPersonEvents();
        }

        private void AddEventButtonClick(object sender, RoutedEventArgs e)
        {
            AddEvent addEventWindow = DependencyContainer.ServiceProvider.GetRequiredService<AddEvent>();
            addEventWindow.PersonId = this.Id;
            addEventWindow.SuccessfulAdditionEvent += this.AddEventWindowAddEvent;
            addEventWindow.ShowDialog();
        }

        private void AddEventWindowAddEvent(object sender, EventArgs e)
        {
            ProfileWindow profileWindow = DependencyContainer.ServiceProvider.GetRequiredService<ProfileWindow>();
            profileWindow.Id = this.Id;
            profileWindow.Show();
            this.Close();
        }

        private bool isEditMode = false;

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (!this.isEditMode)
            {
                // Перехід у режим редагування
                this.isEditMode = true;

                // Приховуємо текстові блоки та виводимо тексти у відповідні текстові поля для редагування
                this.lastNameTextBox.Visibility = Visibility.Hidden;
                this.firstNameTextBox.Visibility = Visibility.Hidden;
                this.maidenNameTextBox.Visibility = Visibility.Hidden;
                this.otherNameTextBox.Visibility = Visibility.Hidden;
                this.deathPlaceTextBlock.Visibility = Visibility.Hidden;
                this.birthPlaceTextBlock.Visibility = Visibility.Hidden;
                this.deathDateTextBlock.Visibility = Visibility.Hidden;
                this.birthDateTextBlock.Visibility = Visibility.Hidden;

                this.lastNameTextBoxEdit.Text = this.lastNameTextBox.Text;
                this.firstNameTextBoxEdit.Text = this.firstNameTextBox.Text;
                this.maidenNameTextBoxEdit.Text = this.maidenNameTextBox.Text;
                this.otherNameTextBoxEdit.Text = this.otherNameTextBox.Text;
                this.deathPlaceTextBlockEdit.Text = this.deathPlaceTextBlock.Text;
                this.birthPlaceTextBlockEdit.Text = this.birthPlaceTextBlock.Text;
                this.bithDatePicker.Text = this.birthDateTextBlock.Text;
                this.deathDatePicker.Text = this.deathDateTextBlock.Text;


                this.lastNameTextBoxEdit.Visibility = Visibility.Visible;
                this.firstNameTextBoxEdit.Visibility = Visibility.Visible;
                this.maidenNameTextBoxEdit.Visibility = Visibility.Visible;
                this.otherNameTextBoxEdit.Visibility = Visibility.Visible;
                this.bithDatePicker.Visibility = Visibility.Visible;
                this.deathDatePicker.Visibility = Visibility.Visible;
                this.birthPlaceTextBlockEdit.Visibility = Visibility.Visible;
                this.deathPlaceTextBlockEdit.Visibility = Visibility.Visible;

                // Змінюємо вигляд кнопки
                this.editButton.Content = "Зберегти";
            }
            else
            {
                // Вихід з режиму редагування та оновлення інформації
                this.isEditMode = false;

                // Виводимо оновлені дані у відповідні текстові блоки
                this.lastNameTextBox.Text = this.lastNameTextBoxEdit.Text;
                this.firstNameTextBox.Text = this.firstNameTextBoxEdit.Text;
                this.maidenNameTextBox.Text = this.maidenNameTextBoxEdit.Text;
                this.otherNameTextBox.Text = this.otherNameTextBoxEdit.Text;
                this.deathPlaceTextBlock.Text = this.deathPlaceTextBlockEdit.Text;
                this.birthPlaceTextBlock.Text = this.birthPlaceTextBlockEdit.Text;
                this.birthDateTextBlock.Text = this.bithDatePicker.Text;
                this.deathDateTextBlock.Text = this.deathDatePicker.Text;

                // Приховуємо текстові поля для редагування та виводимо текстові блоки
                this.lastNameTextBoxEdit.Visibility = Visibility.Hidden;
                this.firstNameTextBoxEdit.Visibility = Visibility.Hidden;
                this.maidenNameTextBoxEdit.Visibility = Visibility.Hidden;
                this.otherNameTextBoxEdit.Visibility = Visibility.Hidden;
                this.bithDatePicker.Visibility = Visibility.Hidden;
                this.deathDatePicker.Visibility = Visibility.Hidden;
                this.birthPlaceTextBlockEdit.Visibility = Visibility.Hidden;
                this.deathPlaceTextBlockEdit.Visibility = Visibility.Hidden;

                this.lastNameTextBox.Visibility = Visibility.Visible;
                this.firstNameTextBox.Visibility = Visibility.Visible;
                this.maidenNameTextBox.Visibility = Visibility.Visible;
                this.otherNameTextBox.Visibility = Visibility.Visible;
                this.deathPlaceTextBlock.Visibility = Visibility.Visible;
                this.birthPlaceTextBlock.Visibility = Visibility.Visible;
                this.deathDateTextBlock.Visibility = Visibility.Visible;
                this.birthDateTextBlock.Visibility = Visibility.Visible;

                // Змінюємо вигляд кнопки
                this.editButton.Content = "Редагувати";

                // Оновлюємо дані в базі даних (потрібно додати відповідні методи сервісу)
                PersonInformation p = this.personService.GetPersonById(this.id);
                p.LastName = this.lastNameTextBox.Text;
                p.FirstName = this.firstNameTextBox.Text;
                p.MaidenName = this.maidenNameTextBox.Text;
                p.OtherNameVariants = this.otherNameTextBox.Text;
                p.DeathDate = this.ParseDate(this.deathDateTextBlock.Text);
                p.BirthDate = this.ParseDate(this.birthDateTextBlock.Text);
                p.DeathPlace = this.deathPlaceTextBlock.Text;
                p.BirthPlace = this.birthPlaceTextBlock.Text;
                this.personService.UpdatePerson(p);
            }
        }

        private DateOnly? ParseDate(string dateString)
        {
            DateOnly? date;
            string dateWithoutTime = dateString.Split(' ')[0];
            try
            {
                date = DateOnly.ParseExact(dateWithoutTime, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                date = null;
            }

            return date;
        }

        private void AddPhotoButtonClick(object sender, RoutedEventArgs e)
        {
        }

    }
}

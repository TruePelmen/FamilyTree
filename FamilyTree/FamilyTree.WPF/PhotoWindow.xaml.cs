namespace FamilyTree.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.WPF.UserControls;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for PhotoWindow.xaml
    /// </summary>
    public partial class PhotoWindow : Window
    {
        private readonly IMediaService mediaService;
        private readonly IMediaEventService mediaEventService;
        private readonly IMediaPersonService mediaPersonService;
        private readonly IPersonService personService;
        private bool isEditMode;
        private int id;

        public PhotoWindow(IMediaService mediaService, IMediaEventService mediaEventService, IMediaPersonService mediaPersonService, IPersonService personService)
        {
            this.InitializeComponent();
            this.mediaService = mediaService;
            this.mediaEventService = mediaEventService;
            this.mediaPersonService = mediaPersonService;
            this.personService = personService;
        }

        public event EventHandler TransferToAnotherPerson;

        public event EventHandler DeletePhoto;

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.GetPhoto();
                this.GetConnectedPersons();
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

        private void GetPhoto()
        {
            Photo photo = this.mediaService.GetMediaById(this.id);
            if (photo != null)
            {
                this.photo.Source = new BitmapImage(new Uri(photo.FilePath));
            }

            if (photo.Description != null)
            {
                this.descriptionTextBlock.Text = photo.Description;
            }

            if (photo.Date != null)
            {
                this.dateTextBlock.Text = photo.Date.ToString();
            }

            if (photo.Place != null)
            {
                this.placeTextBlock.Text = photo.Place;
            }
        }

        private void GetConnectedPersons()
        {
            List<PersonInformation> persons = new List<PersonInformation>();
            persons.AddRange(this.mediaPersonService.GetAllPersonsIdByMediaId(this.id).Select(photo => this.personService.GetPersonById(photo)));
            persons.AddRange(this.mediaEventService.GetAllPersonsIdForPhotos(this.id).Select(photo => this.personService.GetPersonById(photo)));
            foreach (PersonInformation person in persons)
            {
                PersonRecord personRecord = new PersonRecord(person);
                personRecord.TransferToAnotherPerson += this.PersonRecordTransferToAnotherPerson;
                this.personPanel.Children.Add(personRecord);
            }
        }

        private void PersonRecordTransferToAnotherPerson(object sender, int id)
        {
            ProfileWindow profileWindow = DependencyContainer.ServiceProvider.GetService<ProfileWindow>();
            profileWindow.Id = id;
            profileWindow.Show();
            this.TransferToAnotherPerson?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ви дійсно бажаєте видалити це фото?", "Видалення фото", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.mediaService.DeleteMedia(this.id);
                this.DeletePhoto.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }

        private DateOnly? ParseDate()
        {
            DateOnly? date;
            string dateString = this.dateDatePicker.SelectedDate.ToString();
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

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (!this.isEditMode)
            {
                this.isEditMode = true;
                this.dateTextBlock.Visibility = Visibility.Hidden;
                this.dateDatePicker.Text = this.dateTextBlock.Text;
                this.placeTextBlock.Visibility = Visibility.Hidden;
                this.placeTextBox.Text = this.placeTextBlock.Text;
                this.descriptionTextBlock.Visibility = Visibility.Hidden;
                this.descriptionTextBox.Text = this.descriptionTextBlock.Text;
                this.dateDatePicker.Visibility = Visibility.Visible;
                this.placeTextBox.Visibility = Visibility.Visible;
                this.descriptionTextBox.Visibility = Visibility.Visible;
                this.editButton.Content = "Зберегти";
            }
            else
            {
                this.isEditMode = false;
                this.dateTextBlock.Visibility = Visibility.Visible;
                this.placeTextBlock.Visibility = Visibility.Visible;
                this.descriptionTextBlock.Visibility = Visibility.Visible;
                this.dateDatePicker.Visibility = Visibility.Hidden;
                this.placeTextBox.Visibility = Visibility.Hidden;
                this.descriptionTextBox.Visibility = Visibility.Hidden;
                this.editButton.Content = "Редагувати";
                Photo photo = this.mediaService.GetMediaById(this.id);
                photo.Date = this.ParseDate();
                photo.Place = this.placeTextBox.Text;
                photo.Description = this.descriptionTextBox.Text;
                this.mediaService.UpdateMedia(photo);
                this.GetPhoto();
            }
        }
    }
}

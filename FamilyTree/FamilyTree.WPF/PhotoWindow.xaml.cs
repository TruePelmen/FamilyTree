namespace FamilyTree.WPF
{
    using System;
    using System.Windows;
    using System.Windows.Documents;
    using System.Collections.Generic;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.WPF.UserControls;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for PhotoWindow.xaml
    /// </summary>
    public partial class PhotoWindow : Window
    {
        private readonly IMediaService mediaService;
        private readonly IMediaEventService mediaEventService;
        private readonly IMediaPersonService mediaPersonService;
        private readonly IPersonService personService;
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
    }
}

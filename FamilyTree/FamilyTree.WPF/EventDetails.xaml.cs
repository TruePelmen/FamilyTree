namespace FamilyTree.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.WPF.UserControls;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for EventDetails.xaml
    /// </summary>
    public partial class EventDetails : Window
    {
        private readonly IEventService eventService;
        private readonly IMediaEventService mediaEventService;
        private readonly ISpecialRecordService specialRecordService;
        private readonly IPersonService personService;
        private int eventId;
        private EventInformation eventInformation;

        public EventDetails(IMediaEventService mediaEventService, ISpecialRecordService specialRecordService, IEventService eventService, IPersonService personService)
        {
            this.InitializeComponent();
            this.mediaEventService = mediaEventService;
            this.specialRecordService = specialRecordService;
            this.eventService = eventService;
            this.personService = personService;
        }

        public int EventId
        {
            get
            {
                return this.eventId;
            }

            set
            {
                this.eventId = value;
                this.GetEvent();
                this.GetEventPhotos();
                this.GetSpesialRecords();
            }
        }

        private void GetEvent()
        {
            this.eventInformation = this.eventService.GetEventById(this.eventId);
            this.typeTextBlock.Text = this.eventInformation.FullEventType + " " + this.personService.GetShortInformationAboutPerson(this.eventInformation.PersonId).FullName;
            this.dateTextBlock.Text = this.eventInformation.EventDate?.ToString("dd.MM.yyyy");
            this.placeTextBlock.Text = this.eventInformation.EventPlace;
            this.descriptionTextBlock.Text = this.eventInformation.Description;
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

        private void GetEventPhotos()
        {
            var photos = this.mediaEventService.GetAllPhotosForEvent(this.eventId);
            foreach (Photo photo in photos)
            {
                PhotoCard photoCard = new PhotoCard();
                photoCard.RenewCard(photo);
                photoCard.Width = 50;
                photoCard.Height = 100;
                photoCard.Margin = new Thickness(20, 0, 0, 0);
                photoCard.OpenPhotoWindow += this.PhotoCardOpenPhotoWindow;
                this.photosPanel.Children.Add(photoCard);
            }
        }

        private void PhotoCardOpenPhotoWindow(object sender, int id)
        {
            PhotoWindow photoWindow = DependencyContainer.ServiceProvider.GetRequiredService<PhotoWindow>();
            photoWindow.Id = id;
            photoWindow.ShowDialog();
        }

        private void GetSpesialRecords()
        {
           var specialRecords = this.specialRecordService.GetAllSpecialRecordsForEvent(this.eventId);
           int i = 1;
           foreach (SpecialRecordInformation specialRecord in specialRecords)
            {
                SpecialRecord specialRecordCard = new SpecialRecord(specialRecord);
                if (i % 2 == 0)
                {
                    specialRecordCard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDD7CB"));
                }
                else
                {
                    specialRecordCard.Background = Brushes.White;
                }

                var addSpecialRecord = this.addSpecialRecord;
                this.specialRecorsPanel.Children.Remove(addSpecialRecord);
                this.specialRecorsPanel.Children.Add(specialRecordCard);
                this.specialRecorsPanel.Children.Add(addSpecialRecord);
                i++;
            }
        }

        private void AddSpecialRecordButtonClick(object sender, RoutedEventArgs e)
        {
        }
    }
}

namespace FamilyTree.WPF
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
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
        private bool isEditMode;
        private EventInformation eventInformation;

        public EventDetails(IMediaEventService mediaEventService, ISpecialRecordService specialRecordService, IEventService eventService, IPersonService personService)
        {
            this.InitializeComponent();
            this.mediaEventService = mediaEventService;
            this.specialRecordService = specialRecordService;
            this.eventService = eventService;
            this.personService = personService;
        }

        public event EventHandler UpdateEvent;

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
            this.typeTextBlock.Text = this.eventInformation.FullEventType + " " + this.personService.GetPersonById(this.eventInformation.PersonId).FullName;
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
                SpecialRecord specialRecordCard = DependencyContainer.ServiceProvider.GetRequiredService<SpecialRecord>();
                specialRecordCard.SpecialRecordInfo = specialRecord;
                if (i % 2 == 0)
                {
                    specialRecordCard.infoPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDD7CB"));
                }
                else
                {
                    specialRecordCard.infoPanel.Background = Brushes.White;
                }

                var addSpecialRecord = this.addSpecialRecord;
                specialRecordCard.Delete += this.DeleteSpecialRecord;
                specialRecordCard.Update += this.UpdateSpecialRecord;
                this.specialRecorsPanel.Children.Remove(addSpecialRecord);
                this.specialRecorsPanel.Children.Add(specialRecordCard);
                this.specialRecorsPanel.Children.Add(addSpecialRecord);
                i++;
            }
        }

        private void DeleteSpecialRecord(object sender, int id)
        {
            this.specialRecordService.DeleteSpecialRecord(id);
            this.specialRecorsPanel.Children.Remove(this.addSpecialRecord);
            this.specialRecorsPanel.Children.Clear();
            this.specialRecorsPanel.Children.Add(this.addSpecialRecord);
            this.GetSpesialRecords();
        }

        private void UpdateSpecialRecord(object sender, int id)
        {
            this.specialRecorsPanel.Children.Remove(this.addSpecialRecord);
            this.specialRecorsPanel.Children.Clear();
            this.specialRecorsPanel.Children.Add(this.addSpecialRecord);
            this.GetSpesialRecords();
        }

        private void AddSpecialRecordButtonClick(object sender, RoutedEventArgs e)
        {
            AddSpecialRecord addSpecialRecord = DependencyContainer.ServiceProvider.GetRequiredService<AddSpecialRecord>();
            addSpecialRecord.EventId = this.eventId;
            addSpecialRecord.AddSpecialRecordEvent += this.AddSpecialRecord;
            addSpecialRecord.ShowDialog();
        }

        private void AddSpecialRecord(object sender, EventArgs e)
        {
            EventDetails eventDetails = DependencyContainer.ServiceProvider.GetRequiredService<EventDetails>();
            eventDetails.EventId = this.eventId;
            eventDetails.ShowDialog();
            this.Close();
        }

        private DateOnly? ParseDate()
        {
            if (this.dateTextBox.Text != string.Empty)
            {
                string[] date = this.dateTextBox.Text.Split('.');
                return new DateOnly(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            }
            else
            {
                return null;
            }
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (!this.isEditMode)
            {
                this.isEditMode = true;
                this.dateTextBlock.Visibility = Visibility.Hidden;
                this.dateTextBox.Text = this.dateTextBlock.Text;
                this.placeTextBlock.Visibility = Visibility.Hidden;
                this.placeTextBox.Text = this.placeTextBlock.Text;
                this.descriptionTextBlock.Visibility = Visibility.Hidden;
                this.descriptionTextBox.Text = this.descriptionTextBlock.Text;
                this.dateTextBox.Visibility = Visibility.Visible;
                this.placeTextBox.Visibility = Visibility.Visible;
                this.descriptionTextBox.Visibility = Visibility.Visible;
                this.editButton.Content = "Зберегти";
            }
            else
            {
                this.isEditMode = false;
                this.dateTextBlock.Visibility = Visibility.Visible;
                this.dateTextBlock.Text = this.dateTextBox.Text;
                this.placeTextBlock.Visibility = Visibility.Visible;
                this.placeTextBlock.Text = this.placeTextBox.Text;
                this.descriptionTextBlock.Visibility = Visibility.Visible;
                this.descriptionTextBlock.Text = this.descriptionTextBox.Text;
                this.dateTextBox.Visibility = Visibility.Hidden;
                this.placeTextBox.Visibility = Visibility.Hidden;
                this.descriptionTextBox.Visibility = Visibility.Hidden;
                this.editButton.Content = "Редагувати";
                this.eventInformation.EventDate = this.ParseDate();
                this.eventInformation.EventPlace = this.placeTextBox.Text;
                this.eventInformation.Description = this.descriptionTextBox.Text;
                this.eventService.UpdateEvent(this.eventInformation);
                this.UpdateEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

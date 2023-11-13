namespace FamilyTree.WPF
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {
        private readonly IEventService eventService;
        private readonly IPersonService personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddEvent"/> class.
        /// </summary>
        /// <param name="eventService">EventService</param>
        /// <param name="personService">PersonService</param>
        public AddEvent(IEventService eventService, IPersonService personService)
        {
            this.InitializeComponent();
            this.eventService = eventService;
            this.personService = personService;
        }

        public event EventHandler SuccessfulAdditionEvent;

        public int PersonId { get; set; } = 1;

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
            Application.Current.Shutdown();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримуємо значення з полів
            string eventType;
            switch (((ComboBoxItem)this.eventTypeComboBox.SelectedItem).Content.ToString())
            {
                case "Народження":
                    eventType = "birth";
                    break;
                case "Смерть":
                    eventType = "death";
                    break;
                case "Одруження":
                    eventType = "marriage";
                    break;
                case "Інша подія":
                    eventType = "other event";
                    break;
                default:
                    eventType = "other event";
                    break;
            }

            bool isEventUnique = this.eventService.IsEventUnique(this.PersonId, eventType);
            DateOnly? eventDate = this.ParseDate(); // Використовуйте DateOnly?
            int id = this.PersonId;
            string eventPlace = this.eventPlaceTextBox.Text;
            string eventDescription = this.eventDescriptionTextBox.Text;
            int eventAge = eventDate.Value.Year - this.personService.GetPersonById(this.PersonId).BirthDate.Value.Year;
            if (eventAge < 0)
            {
                MessageBox.Show("Ця особа не могла померти перш, ніж народитись!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (isEventUnique)
            {
                EventInformation eventInformation = new EventInformation()
                {
                    EventType = eventType,
                    EventDate = eventDate,
                    EventPlace = eventPlace,
                    PersonId = id,
                    Description = eventDescription,
                    Age = eventAge,
                };
                this.eventService.AddEvent(eventInformation);

                this.SuccessfulAdditionEvent?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Для цієї особи вже є подія типу {eventType}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private DateOnly? ParseDate()
        {
            string dateString = this.eventDatePicker.SelectedDate.ToString();
            string dateWithoutTime = dateString.Split(' ')[0];
            return DateOnly.ParseExact(dateWithoutTime, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
    }
}

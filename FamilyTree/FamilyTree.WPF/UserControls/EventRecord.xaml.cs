namespace FamilyTree.WPF.UserControls
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using FamilyTree.BLL;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for EventRecord.xaml
    /// </summary>
    public partial class EventRecord : UserControl
    {
        private int eventid;

        public EventRecord(EventInformation eventInformation)
        {
            this.InitializeComponent();
            this.eventid = eventInformation.Id;
            if (eventInformation.EventDate != null)
            {
                this.year.Text = eventInformation.EventDate?.ToString("yyyy");
                this.date.Text = eventInformation.EventDate?.ToString("dd.MM.yyyy");
            }

            if (eventInformation.Age != null)
            {
                if (eventInformation.Age == 0)
                {
                    this.age.Text = "-";
                }
                else if (eventInformation.Age != 0)
                {
                    this.age.Text = eventInformation.Age.ToString();
                }
            }

            if (eventInformation.EventType != null)
            {
                this.type.Text = eventInformation.FullEventType;
            }

            if (eventInformation.EventPlace != null)
            {
                this.place.Text = eventInformation.EventPlace;
            }
        }

        public event EventHandler DeleteEvent;

        public event EventHandler UpdateEvent;

        public int Id
        {
            get
            {
                return this.eventid;
            }
        }

        private void UserControlMouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDD7CB"));
        }

        private void UserControlMouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.White;
        }

        private void UserControlMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventDetails eventWindow = DependencyContainer.ServiceProvider.GetRequiredService<EventDetails>();
            eventWindow.UpdateEvent += this.UpdateEvent;
            eventWindow.EventId = this.eventid;
            eventWindow.ShowDialog();
        }

        private void DeleteButtonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ви дійсно бажаєте видалити цю подію?", "Видалення події", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.DeleteEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

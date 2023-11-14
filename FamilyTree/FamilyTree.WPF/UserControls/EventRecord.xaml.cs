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
                this.age.Text = eventInformation.Age.ToString();
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
            eventWindow.EventId = this.eventid;
            eventWindow.ShowDialog();
        }
    }
}

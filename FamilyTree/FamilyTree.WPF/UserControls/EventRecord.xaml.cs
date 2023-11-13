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

    /// <summary>
    /// Interaction logic for EventRecord.xaml
    /// </summary>
    public partial class EventRecord : UserControl
    {
        public EventRecord(EventInformation eventInformation)
        {
            this.InitializeComponent();
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
    }
}

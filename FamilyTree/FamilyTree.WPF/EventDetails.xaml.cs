namespace FamilyTree.WPF
{
    using FamilyTree.BLL.Interfaces;
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

    /// <summary>
    /// Interaction logic for EventDetails.xaml
    /// </summary>
    public partial class EventDetails : Window
    {
        private readonly IEventService eventService;
        private readonly IMediaEventService mediaEventService;
        private readonly ISpecialRecordService specialRecordService;
        private int eventId;

        public EventDetails(IMediaEventService mediaEventService, ISpecialRecordService specialRecordService, IEventService eventService)
        {
            this.InitializeComponent();
            this.mediaEventService = mediaEventService;
            this.specialRecordService = specialRecordService;
            this.eventService = eventService;
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
            Application.Current.Shutdown();
        }
    }
}

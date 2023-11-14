namespace FamilyTree.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Interaction logic for AddSpecialRecord.xaml
    /// </summary>
    public partial class AddSpecialRecord : Window
    {
        private readonly IEventService eventService;
        private readonly IPersonService personService;
        private readonly ISpecialRecordService specialRecordService;
        private int eventId = 7;

        public AddSpecialRecord(IEventService eventService, IPersonService personService, ISpecialRecordService specialRecordService)
        {
            this.InitializeComponent();
            this.eventService = eventService;
            this.personService = personService;
            this.specialRecordService = specialRecordService;
        }

        public event EventHandler AddSpecialRecordEvent;

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
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string recordType;
            ComboBoxItem selectedItem = (ComboBoxItem)recordTypeComboBox.SelectedItem;
            switch (selectedItem.Content.ToString())
            {
                case "Метрична книга":
                    recordType = "metric book";
                    break;
                case "Сповідальний запис":
                    recordType = "confessional record";
                    break;
                case "Переписний запис":
                    recordType = "census record";
                    break;
                case "Перепис населння":
                    recordType = "population census";
                    break;
                default:
                    recordType = "population census";
                    break;
            }
            if (this.specialRecordService.AreSpecialRecordsOfTypeExistForEvent(EventId, recordType))
            {
                MessageBox.Show($"Для цієї події вже існують записи типу '{recordType}'. Додавання нового запису заборонено.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int? houseNumber = int.Parse(this.recordPlaceTextBox.Text);
            string priest = this.recordPriestTextBox.Text;
            string record = this.recordDescTextBox.Text;
            SpecialRecordInformation specialRecord = new SpecialRecordInformation()
            {
                RecordType = recordType,
                HouseNumber = houseNumber,
                Priest = priest,
                EventId = this.EventId,
            };
            this.specialRecordService.AddSpecialRecord(specialRecord);
            this.AddSpecialRecordEvent.Invoke(this, new EventArgs());
            this.Close();
        }

        private void RecordTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)this.recordTypeComboBox.SelectedItem;

            if (selectedItem.Content.ToString() == "Метрична книга" || selectedItem.Content.ToString() == "Сповідальний запис")
            {
                this.priestStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.priestStackPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}

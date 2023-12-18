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
    using FamilyTree.BLL.Interfaces;

    /// <summary>
    /// Interaction logic for SpecialRecord.xaml
    /// </summary>
    public partial class SpecialRecord : UserControl
    {
        private readonly ISpecialRecordService specialRecordService;
        private SpecialRecordInformation specialRecord;

        public SpecialRecord(ISpecialRecordService specialRecordService)
        {
            this.specialRecordService = specialRecordService;
            this.InitializeComponent();
        }

        public event EventHandler<int> Delete;

        public SpecialRecordInformation SpecialRecordInfo
        {
            get
            {
                return this.specialRecord;
            }

            set
            {
                this.specialRecord = value;
                this.typeTextBlock.Text = this.specialRecord.FullRecordType;
                if (this.specialRecord.HouseNumber != null)
                {
                    this.numberTextBlock.Text = this.specialRecord.HouseNumber.ToString();
                }

                if (this.specialRecord.Priest != null)
                {
                    this.priestTextBlock.Text = this.specialRecord.Priest;
                }

                if (this.specialRecord.Record != null)
                {
                    this.descriptionTextBlock.Text = this.specialRecord.Record;
                }
            }
        }

        private void DeleteButtonMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.Delete?.Invoke(this, this.specialRecord.Id);
        }

        private void EditButtonMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.descriptionTextBlock.Visibility = Visibility.Hidden;
            this.descriptionTextBox.Visibility = Visibility.Visible;
            this.descriptionTextBox.Text = this.descriptionTextBlock.Text;
            this.numberTextBlock.Visibility = Visibility.Hidden;
            this.numberTextBox.Visibility = Visibility.Visible;
            this.numberTextBox.Text = this.numberTextBlock.Text;
            this.priestTextBlock.Visibility = Visibility.Hidden;
            this.priestTextBox.Visibility = Visibility.Visible;
            this.priestTextBox.Text = this.priestTextBlock.Text;
            this.editPanel.Visibility = Visibility.Hidden;
            this.savePanel.Visibility = Visibility.Visible;
        }

        private void ShowInfoPanel()
        {
            this.descriptionTextBlock.Visibility = Visibility.Visible;
            this.descriptionTextBox.Visibility = Visibility.Hidden;
            this.numberTextBlock.Visibility = Visibility.Visible;
            this.numberTextBox.Visibility = Visibility.Hidden;
            this.priestTextBlock.Visibility = Visibility.Visible;
            this.priestTextBox.Visibility = Visibility.Hidden;
            this.editPanel.Visibility = Visibility.Visible;
            this.savePanel.Visibility = Visibility.Hidden;
        }

        private void BackButtonMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.ShowInfoPanel();
        }

        private void SaveButtonMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            SpecialRecordInformation specialRecord = new SpecialRecordInformation
            {
                Id = this.specialRecord.Id,
                RecordType = this.specialRecord.RecordType,
                HouseNumber = int.Parse(this.numberTextBox.Text),
                Priest = this.priestTextBox.Text,
                Record = this.descriptionTextBox.Text,
                EventId = this.specialRecord.EventId,
            };
            this.specialRecordService.UpdateSpecialRecord(specialRecord);
            this.SpecialRecordInfo = specialRecord;
            this.ShowInfoPanel();
        }
    }
}

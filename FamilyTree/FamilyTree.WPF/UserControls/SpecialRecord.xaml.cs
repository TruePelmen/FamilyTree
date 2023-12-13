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
    /// Interaction logic for SpecialRecord.xaml
    /// </summary>
    public partial class SpecialRecord : UserControl
    {
        public SpecialRecord(SpecialRecordInformation specialRecord)
        {
            this.InitializeComponent();
            this.typeTextBlock.Text = specialRecord.FullRecordType;
            if (specialRecord.HouseNumber != null)
            {
                this.numberTextBlock.Text = specialRecord.HouseNumber.ToString();
            }

            if (specialRecord.Priest != null)
            {
                this.priestTextBlock.Text = specialRecord.Priest;
            }

            if (specialRecord.Record != null)
            {
                this.descriptionTextBlock.Text = specialRecord.Record;
            }
        }
    }
}

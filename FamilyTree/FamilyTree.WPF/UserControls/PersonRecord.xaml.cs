namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for PersonRecord.xaml
    /// </summary>
    public partial class PersonRecord : UserControl
    {
        private int id;

        public PersonRecord(PersonInformation person)
        {
            this.InitializeComponent();
            this.id = person.Id;
            this.nameTextBox.Text = person.FullName;
            this.photo.Source = new BitmapImage(new Uri(person.MainPhoto));
        }

        public event EventHandler<int> TransferToAnotherPerson;

        private void UserControlMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.TransferToAnotherPerson?.Invoke(this, this.id);
        }
    }
}

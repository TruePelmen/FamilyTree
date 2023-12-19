namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for PhotoCard.xaml
    /// </summary>
    public partial class PhotoCard : UserControl
    {
        public PhotoCard()
        {
            this.InitializeComponent();
        }

        public event EventHandler<int> OpenPhotoWindow;

        public int Id { get; set; }

        public void RenewCard(Photo photo)
        {
            this.Id = photo.Id;
            this.photo.Source = new BitmapImage(new System.Uri(photo.FilePath));
        }

        private void UserControlMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.OpenPhotoWindow?.Invoke(this, this.Id);
        }
    }
}

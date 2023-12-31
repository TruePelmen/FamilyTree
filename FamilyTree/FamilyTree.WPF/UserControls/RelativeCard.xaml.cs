﻿namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using FamilyTree.BLL;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for RelativeCard.xaml
    /// </summary>
    public partial class RelativeCard : UserControl
    {
        private int idPerson;

        public RelativeCard()
        {
            this.InitializeComponent();
        }

        public event EventHandler<int> TransferToAnotherPerson;

        public void RenewCardInformation(PersonInformation person, string relative)
        {
            this.idPerson = person.Id;
            this.lastNameTextBox.Text = person.LastName;
            this.firstNameTextBox.Text = person.FirstName;
            this.relativeTextBox.Text = relative;
            if (person.MainPhoto != null)
            {
                this.photo.Source = new BitmapImage(new Uri(person.MainPhoto));
            }
            else
            {
                this.DefaultPhoto(relative);
            }
        }

        private void UserControlMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.TransferToAnotherPerson?.Invoke(this, this.idPerson);
        }

        private void DefaultPhoto(string relative)
        {
            string relativePart = relative.Split(' ')[1];
            if (relativePart == "чоловік" || relativePart == "батько" || relativePart == "син" || relativePart == "брат")
            {
                this.photo.Source = this.ChangeMainPhoto("man.png");
            }
            else
            {
                this.photo.Source = this.ChangeMainPhoto("woman.png");
            }
        }

        private ImageSource ChangeMainPhoto(string mainPhotoRelativePath)
        {
            string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string relativePath = Path.Combine("../../..", "Images", mainPhotoRelativePath);
            string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));
            return new BitmapImage(new Uri(fullPath));
        }

        private void UserControlMouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDD7CB"));
        }

        private void UserControlMouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.White;
        }
    }
}

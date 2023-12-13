namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.BLL.Interfeces;

    /// <summary>
    /// Interaction logic for UserRecord.xaml
    /// </summary>
    public partial class UserRecord : UserControl
    {
        private readonly IUserService userService;
        private readonly IUserTreeService userTreeService;
        private string userLogin;
        private string accessType;
        private bool isEditable;

        public UserRecord(IUserService userService, IUserTreeService userTreeService)
        {
            this.InitializeComponent();
            this.userService = userService;
            this.userTreeService = userTreeService;
        }

        public event EventHandler DeleteUserRecord;

        public event EventHandler EditUserRecord;

        public int Id { get; set; }

        public bool IsEditable
        {
            get
            {
                return this.isEditable;
            }

            set
            {
                this.isEditable = value;
                this.editPanel.Visibility = this.isEditable ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public string UserLogin
        {
            get
            {
                return this.userLogin;
            }

            set
            {
                this.userLogin = value;
                this.userLoginTextBlock.Text = value;
            }
        }

        public string AccessType
        {
            get
            {
                return this.accessType;
            }

            set
            {
                this.accessType = value;
                if (this.accessType == "edit")
                {
                    this.accessTypeTextBlock.Text = "Редагування";
                    this.IsEditable = true;
                }
                else if (this.accessType == "view")
                {
                    this.accessTypeTextBlock.Text = "Перегляд";
                    this.IsEditable = false;
                }
            }
        }

        private void EditButtonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.editPanel.Visibility = Visibility.Hidden;
            this.savePanel.Visibility = Visibility.Visible;
            this.userLoginTextBlock.Visibility = Visibility.Hidden;
            this.userLoginTextBox.Visibility = Visibility.Visible;
            this.userLoginTextBox.Text = this.userLogin;
            this.accessTypeTextBlock.Visibility = Visibility.Hidden;
            this.accessTypeComboBox.Visibility = Visibility.Visible;
        }

        private void DeleteButtonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var userTree = this.userTreeService.GetUserTreeById(this.Id);
            this.userTreeService.DeleteUserTree(userTree.TreeId, userTree.UserLogin);
            this.DeleteUserRecord?.Invoke(this, e);
        }

        private void SaveButtonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.userLoginTextBox.Text != string.Empty && !this.userService.FindUserByLogin(this.userLoginTextBox.Text))
            {
                this.UserLogin = this.userLoginTextBox.Text;
            }

            if (this.accessTypeComboBox.SelectedIndex == 0)
            {
                this.AccessType = "edit";
            }
            else if (this.accessTypeComboBox.SelectedIndex == 1)
            {
                this.AccessType = "view";
            }

            this.editPanel.Visibility = Visibility.Visible;
            this.savePanel.Visibility = Visibility.Hidden;
            this.userLoginTextBlock.Visibility = Visibility.Visible;
            this.userLoginTextBox.Visibility = Visibility.Hidden;
            this.accessTypeTextBlock.Visibility = Visibility.Visible;
            this.accessTypeComboBox.Visibility = Visibility.Hidden;
            this.EditUserRecord?.Invoke(this, e);
        }

        private void BackButtonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.editPanel.Visibility = Visibility.Visible;
            this.savePanel.Visibility = Visibility.Hidden;
            this.userLoginTextBlock.Visibility = Visibility.Visible;
            this.userLoginTextBox.Visibility = Visibility.Hidden;
            this.accessTypeTextBlock.Visibility = Visibility.Visible;
            this.accessTypeComboBox.Visibility = Visibility.Hidden;
        }
    }
}

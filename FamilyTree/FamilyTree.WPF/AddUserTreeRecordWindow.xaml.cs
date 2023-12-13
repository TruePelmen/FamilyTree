namespace FamilyTree.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using Serilog;

    /// <summary>
    /// Interaction logic for AddUserTreeRecordWindow.xaml
    /// </summary>
    public partial class AddUserTreeRecordWindow : Window
    {
        private readonly IUserTreeService userTreeService;
        private string accessType;
        private int treeId;
        private List<string> usersList;

        public AddUserTreeRecordWindow(IUserTreeService userTreeService)
        {
            this.InitializeComponent();
            this.userTreeService = userTreeService;
        }

        public event EventHandler SuccessfulAdditionRecord;

        public int TreeId
        {
            get
            {
                return this.treeId;
            }

            set
            {
                this.treeId = value;
                this.usersList = this.userTreeService.GetFreeUsersLoginByTreeId(this.treeId).ToList();
                this.userComboBox.ItemsSource = this.usersList;
                this.userComboBox.SelectedItem = this.usersList.FirstOrDefault();
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            switch (((ComboBoxItem)this.accessTypeComboBox.SelectedItem).Content.ToString())
            {
                case "Редагування":
                    this.accessType = "edit";
                    break;
                case "Перегляд":
                    this.accessType = "view";
                    break;
            }

            this.userTreeService.AddUserTree(this.userComboBox.SelectedItem.ToString(), this.TreeId, this.accessType);
            Log.Information("UserTree record was successfully added =)");
            this.SuccessfulAdditionRecord?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
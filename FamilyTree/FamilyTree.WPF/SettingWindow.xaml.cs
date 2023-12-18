namespace FamilyTree.WPF
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
    using System.Windows.Shapes;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.WPF.UserControls;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private readonly IUserTreeService userTreeService;
        private readonly ITreeService treeService;
        private readonly ITreePersonService treePersonService;
        private string userLogin;
        private int treeId;
        private bool isEditable;

        public SettingWindow(IUserTreeService userTreeService, ITreePersonService treePersonService, ITreeService treeService)
        {
            this.InitializeComponent();
            this.userTreeService = userTreeService;
            this.treePersonService = treePersonService;
            this.treeService = treeService;
        }

        public event EventHandler CloseWindow;

        public string UserLogin
        {
            get
            {
                return this.userLogin;
            }

            set
            {
                this.userLogin = value;
                this.userLoginBlock.Text = this.userLogin;
                this.TreeId = this.userTreeService.GetAllTreeByUserLogin(this.userLogin).FirstOrDefault().Id;
                this.FillTreesList();
            }
        }

        public int TreeId
        {
            get
            {
                return this.treeId;
            }

            set
            {
                this.treeId = value;
                this.FillTreeInformation();
                this.FillUserRecordsList();
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

        private void WindowMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void FillTreeInformation()
        {
            if (this.userTreeService.GetAccessTypeByUserLoginAndTreeId(this.userLogin, this.treeId) == "edit")
            {
                this.accessType.Text = "Редагування";
                this.addUser.Visibility = Visibility.Visible;
                this.isEditable = true;
            }
            else
            {
                this.accessType.Text = "Перегляд";
                this.addUser.Visibility = Visibility.Hidden;
                this.isEditable = false;
            }

            this.personsNumber.Text = this.treePersonService.GetPersonsNumber(this.TreeId).ToString();
            this.eventsNumber.Text = this.treePersonService.GetEventsNumber(this.TreeId).ToString();
            this.photosNumber.Text = this.treePersonService.GetPhotosNumber(this.TreeId).ToString();
        }

        private void FillTreesList()
        {
            var trees = this.userTreeService.GetAllTreeByUserLogin(this.UserLogin);
            this.listOfTrees.DisplayMemberPath = "Name";
            this.listOfTrees.SelectedValuePath = "Id";
            this.listOfTrees.ItemsSource = trees;
            this.listOfTrees.SelectedIndex = 0;
        }

        private void FillUserRecordsList()
        {
            this.userRecordsPanel.Children.Clear();
            var userRecords = this.userTreeService.GetAllUserTreeByTreeId(this.TreeId);
            foreach (var userRecord in userRecords)
            {
                var userRecordControl = DependencyContainer.ServiceProvider.GetRequiredService<UserRecord>();
                userRecordControl.IsEditable = this.isEditable;
                userRecordControl.Id = userRecord.Id;
                userRecordControl.UserLogin = userRecord.UserLogin;
                userRecordControl.AccessType = userRecord.AccessType;
                userRecordControl.DeleteUserRecord += this.UserRecordControlDeleteUserRecord;
                userRecordControl.EditUserRecord += this.UserRecordControlEditUserRecord;
                this.userRecordsPanel.Children.Add(userRecordControl);
            }
        }

        private void ListOfTreesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listOfTrees.SelectedItem != null)
            {
                int selectedTreeId = (int)this.listOfTrees.SelectedValue;
                this.TreeId = selectedTreeId;
            }
        }

        private void AddTreeClick(object sender, RoutedEventArgs e)
        {
            AddTree addTree = DependencyContainer.ServiceProvider.GetRequiredService<AddTree>();
            addTree.TreeCreated += this.TreeCreated;
            addTree.ShowDialog();
        }

        private void TreeCreated(object sender, EventArgs e)
        {
            this.userTreeService.AddUserTree(this.userLogin, (sender as AddTree).TreeId, "edit");
            this.FillTreesList();
            this.listOfTrees.SelectedIndex = this.listOfTrees.Items.Count - 1;
        }

        private void UserLoginBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EditUserWindow editUserWindow = DependencyContainer.ServiceProvider.GetRequiredService<EditUserWindow>();
            editUserWindow.UserLogin = this.userLogin;
            editUserWindow.Exit += this.EditUserWindowExit;
            editUserWindow.ShowDialog();
        }

        private void EditUserWindowExit(object sender, EventArgs e)
        {
            this.CloseWindow?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void DeleteTreeClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете видалити дерево?", "Видалення дерева", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (this.isEditable)
                {
                    this.treeService.DeleteTree(this.TreeId);
                }
                else
                {
                    this.userTreeService.DeleteUserTree(this.TreeId, this.userLogin);
                }

                this.FillTreesList();
                this.listOfTrees.SelectedIndex = 0;
            }
        }

        private void UserRecordControlEditUserRecord(object sender, EventArgs e)
        {
            UserRecord userRecord = (UserRecord)sender;
            this.userTreeService.UpdateUserTree(userRecord.Id, userRecord.UserLogin, this.TreeId, userRecord.AccessType);
            this.FillTreeInformation();
        }

        private void UserRecordControlDeleteUserRecord(object sender, EventArgs e)
        {
            UserRecord userRecord = (UserRecord)sender;
            this.userRecordsPanel.Children.Remove(userRecord);
        }

        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            AddUserTreeRecordWindow addUserTreeRecordWindow = DependencyContainer.ServiceProvider.GetRequiredService<AddUserTreeRecordWindow>();
            addUserTreeRecordWindow.ViewModel.TreeId = this.TreeId;
            addUserTreeRecordWindow.ViewModel.SuccessfulAdditionRecord += this.AddUserTreeRecordWindowSuccessfulAdditionRecord;
            addUserTreeRecordWindow.ShowDialog();
        }

        private void AddUserTreeRecordWindowSuccessfulAdditionRecord(object sender, EventArgs e)
        {
            this.FillUserRecordsList();
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = DependencyContainer.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.UserLogin = this.userLogin;
            mainWindow.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Statistics statistics = DependencyContainer.ServiceProvider.GetRequiredService<Statistics>();
            statistics.TreeID = this.treeId;
            statistics.ShowDialog();
        }
    }
}

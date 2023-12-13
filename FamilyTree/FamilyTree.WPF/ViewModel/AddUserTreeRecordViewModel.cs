namespace FamilyTree.WPF.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using FamilyTree.BLL.Interfaces;
    using Serilog;

    public class AddUserTreeRecordViewModel : ViewModelBase
    {
        private readonly IUserTreeService userTreeService;
        private int treeId;
        private List<string> usersList;
        private string selectedUser;
        private string accessType;

        public AddUserTreeRecordViewModel(IUserTreeService userTreeService)
        {
            this.userTreeService = userTreeService;
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }

        public int TreeId
        {
            get { return treeId; }
            set
            {
                treeId = value;
                UsersList = userTreeService.GetFreeUsersLoginByTreeId(treeId).ToList();
                SelectedUser = UsersList.FirstOrDefault();
            }
        }

        public List<string> UsersList
        {
            get { return usersList; }
            set
            {
                usersList = value;
                OnPropertyChanged();
            }
        }

        public string SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        public string AccessType
        {
            get { return accessType; }
            set
            {
                accessType = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private bool CanSave(object parameter) => true;

        private void Save(object parameter)
        {
            switch (AccessType)
            {
                case "Редагування":
                    AccessType = "edit";
                    break;
                case "Перегляд":
                    AccessType = "view";
                    break;
            }

            userTreeService.AddUserTree(SelectedUser, TreeId, AccessType);
            Log.Information("UserTree record was successfully added =)");
            SuccessfulAdditionRecord?.Invoke(this, EventArgs.Empty);
            CloseAction?.Invoke();
        }

        private void Cancel(object parameter)
        {
            CloseAction?.Invoke();
        }

        public event EventHandler SuccessfulAdditionRecord;
        public Action CloseAction { get; set; }
    }
}


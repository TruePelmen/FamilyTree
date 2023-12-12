using FamilyTree.BLL.Interfaces;
using Serilog;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace FamilyTree.WPF.ViewModel
{
    public class AddUserTreeRecordViewModel : INotifyPropertyChanged
    {
        private readonly IUserTreeService userTreeService;

        public AddUserTreeRecordViewModel(IUserTreeService userTreeService)
        {
            this.userTreeService = userTreeService;
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }

        private int treeId;
        public int TreeId
        {
            get { return treeId; }
            set
            {
                treeId = value;
                OnPropertyChanged();
                UsersList = userTreeService.GetFreeUsersLoginByTreeId(treeId).ToList();
                SelectedUser = UsersList.FirstOrDefault();
            }
        }

        private List<string> usersList;
        public List<string> UsersList
        {
            get { return usersList; }
            set
            {
                usersList = value;
                OnPropertyChanged();
            }
        }

        private string selectedUser;
        public string SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        private string accessType;
        public string AccessType
        {
            get { return accessType; }
            set
            {
                accessType = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler SuccessfulAdditionRecord;
        public Action CloseAction { get; set; }
    }
}

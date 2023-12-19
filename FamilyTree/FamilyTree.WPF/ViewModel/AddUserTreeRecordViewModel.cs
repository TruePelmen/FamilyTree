namespace FamilyTree.WPF.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using FamilyTree.BLL.Interfaces;
    using Serilog;
    using YourNamespace;

    public class AddUserTreeRecordViewModel : INotifyPropertyChanged
    {
        private readonly IUserTreeService userTreeService;
        private int treeId;
        private List<string> usersList;
        private string selectedUser;
        private string accessType = "edit";
        private bool controlsEnabled = true;

        public AddUserTreeRecordViewModel(IUserTreeService userTreeService)
        {
            this.userTreeService = userTreeService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SuccessfulAdditionRecord;

        public int TreeId
        {
            get
            {
                return this.treeId;
            }

            set
            {
                if (this.treeId != value)
                {
                    this.treeId = value;
                    this.OnPropertyChanged();
                    this.LoadUsersList();
                }
            }
        }

        public List<string> UsersList
        {
            get
            {
                return this.usersList;
            }

            set
            {
                if (this.usersList != value)
                {
                    this.usersList = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedUser
        {
            get
            {
                return this.selectedUser;
            }

            set
            {
                if (this.selectedUser != value)
                {
                    this.selectedUser = value;
                    this.OnPropertyChanged();
                }
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
                if (this.accessType != value)
                {
                    this.accessType = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool ControlsEnabled
        {
            get
            {
                return this.controlsEnabled;
            }

            set
            {
                if (this.controlsEnabled != value)
                {
                    this.controlsEnabled = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand => new RelayCommand(this.Save);

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadUsersList()
        {
            this.UsersList = this.userTreeService.GetFreeUsersLoginByTreeId(this.TreeId).ToList();
            this.SelectedUser = this.UsersList.FirstOrDefault();
        }

        private void Save()
        {
            this.userTreeService.AddUserTree(this.SelectedUser, this.TreeId, this.AccessType);
            Log.Information("UserTree record was successfully added =)");
            this.SuccessfulAdditionRecord?.Invoke(this, EventArgs.Empty);
        }
    }
}
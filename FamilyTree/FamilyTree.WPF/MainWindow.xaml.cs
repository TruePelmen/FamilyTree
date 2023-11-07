// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FamilyTree.WPF
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ITreePersonService treePersonService;
        private readonly ITreeService treeService;
        private readonly IUserTreeService userTreeService;
        private int treeId;
        private string userLogin;
        private UserControls.Tree familyTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="treePersonService">An instance of the tree person service for managing tree-related person data.</param>
        /// <param name="treeService">An instance of the tree service for managing tree-related operations.</param>
        /// <param name="userTreeService">An instance of the user tree service for managing user-related tree data.</param>
        [System.Obsolete]
        public MainWindow(ITreePersonService treePersonService, ITreeService treeService, IUserTreeService userTreeService)
        {
            this.treeService = treeService;
            this.treePersonService = treePersonService;
            this.userTreeService = userTreeService;
            this.InitializeComponent();
            this.personsList.RowDoubleClick += this.PersonsListRowDoubleClick;
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
                this.treeId = this.userTreeService.GetAllTreeByUserLogin(this.userLogin).FirstOrDefault().Id;
                this.CreateUI();
                this.FillTreesList();
            }
        }

        private void CreateUI()
        {
            this.familyTree = DependencyContainer.ServiceProvider.GetRequiredService<UserControls.Tree>();
            this.familyTree.Margin = new Thickness(50, 20, 0, 0);
            Grid.SetColumn(this.familyTree, 0);
            this.treeGrid.Children.Add(this.familyTree);
            this.FillListOfPerson();
        }

        private void FillTreesList()
        {
            var trees = this.userTreeService.GetAllTreeByUserLogin(this.userLogin);
            this.listOfTrees.DisplayMemberPath = "Name";
            this.listOfTrees.SelectedValuePath = "Id";
            this.listOfTrees.ItemsSource = trees;
            this.listOfTrees.SelectedIndex = 0;
        }

        private void ListOfTreesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listOfTrees.SelectedItem != null)
            {
                int selectedTreeId = (int)this.listOfTrees.SelectedValue;
                this.familyTree.TreeId = selectedTreeId;
                this.familyTree.AceessType = this.userTreeService.GetAccessTypeByUserLoginAndTreeId(this.userLogin, selectedTreeId);
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

        private void FillListOfPerson()
        {
            var persons = this.treePersonService.GetTreePeopleByTreeId(this.treeId).ToList();
            List<PersonCardInformation> formatingPersons = new List<PersonCardInformation>();
            foreach (var person in persons)
            {
                PersonCardInformation personCardInformation = new PersonCardInformation();
                personCardInformation.Person = person;
                formatingPersons.Add(personCardInformation);
            }

            this.personsList.PersonList = formatingPersons;
        }

        private void PersonsListRowDoubleClick(object sender, int selectedId)
        {
            this.familyTree.FocusPersonId = selectedId;
        }
    }
}

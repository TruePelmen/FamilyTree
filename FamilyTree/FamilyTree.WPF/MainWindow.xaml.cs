// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FamilyTree.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Linq;
    using System.Reflection.Metadata;
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
        private readonly IUserTreeService userTreeService;
        private string userLogin;
        private int treeId;
        private UserControls.Tree familyTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="treePersonService">An instance of the tree person service for managing tree-related person data.</param>
        /// <param name="userTreeService">An instance of the user tree service for managing user-related tree data.</param>
        public MainWindow(ITreePersonService treePersonService, IUserTreeService userTreeService)
        {
            this.treePersonService = treePersonService;
            this.userTreeService = userTreeService;
            this.InitializeComponent();
            this.personsList.RowDoubleClick += this.PersonsListRowDoubleClick;
        }

        /// <summary>
        /// Gets or sets the identifier of the tree.
        /// </summary>
        /// <value>
        /// A int representing the tree id.
        /// </value>
        public int TreeId
        {
            get
            {
                return this.treeId;
            }

            set
            {
                this.treeId = value;
                this.familyTree.TreeId = this.treeId;
                this.familyTree.AceessType = this.userTreeService.GetAccessTypeByUserLoginAndTreeId(this.userLogin, this.treeId);
                this.FillListOfPerson();
            }
        }

        /// <summary>
        /// Gets or sets the user's login name.
        /// </summary>
        /// <value>
        /// A string representing the user's login name.
        /// </value>
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
            this.familyTree.TreeChanged += this.FamilyTreeChanged;
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
                this.TreeId = selectedTreeId;
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

        private void FamilyTreeChanged(object sender, EventArgs eventArgs)
        {
            this.FillListOfPerson();
        }
    }
}

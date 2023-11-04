// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FamilyTree.WPF
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using FamilyTree.BLL.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ITreePersonService treePersonService;
        private readonly ITreeService treeService;
        private int treeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="treePersonService">An instance of the tree person service for managing tree-related person data.</param>
        /// <param name="treeService">An instance of the tree service for managing tree-related operations.</param>
        [System.Obsolete]
        public MainWindow(ITreePersonService treePersonService, ITreeService treeService)
        {
            this.treeService = treeService;
            this.treePersonService = treePersonService;
            this.treeId = 2;
            this.InitializeComponent();
            UserControls.Tree familyTree = DependencyContainer.ServiceProvider.GetRequiredService<UserControls.Tree>();
            familyTree.Width = 980;
            familyTree.Height = 600;
            Grid.SetColumn(familyTree, 0);
            this.treeGrid.Children.Add(familyTree);
            this.FillListOfPerson();
            familyTree.FocusPersonId = this.treeService.GetPrimaryPersonId(this.treeId);
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
            this.personsList.personList.ItemsSource = persons;
        }
    }
}

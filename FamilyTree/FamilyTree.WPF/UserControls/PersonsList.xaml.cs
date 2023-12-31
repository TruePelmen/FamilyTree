﻿namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using FamilyTree.BLL;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interaction logic for PersonsList.xaml.
    /// </summary>
    public partial class PersonsList : UserControl
    {
        private List<PersonInformation> personsList;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonsList"/> class.
        /// </summary>
        public PersonsList()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when a row is double-clicked in the data grid.
        /// </summary>
        public event EventHandler<int> RowDoubleClick;

        /// <summary>
        /// Sets the list of PersonCardInformation to be displayed in the data grid.
        /// </summary>
        public List<PersonInformation> PersonList
        {
            set
            {
                this.personsList = value;
                this.personList.ItemsSource = this.personsList;
            }
        }

        private void SearchButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var filtered = this.personsList.ToList();
            if (!string.IsNullOrWhiteSpace(this.searchLastNameTextBox.Text))
            {
                filtered = filtered.Where(x => x.LastName == this.searchLastNameTextBox.Text).ToList();
            }

            if (!string.IsNullOrWhiteSpace(this.searchFirstNameTextBox.Text))
            {
                filtered = filtered.Where(x => x.FirstName == this.searchFirstNameTextBox.Text).ToList();
            }

            this.personList.ItemsSource = filtered;
        }

        private void ResetButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var list = this.personsList.ToList();
            this.personList.ItemsSource = list;
        }

        private void PersonListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedRow = this.personList.SelectedItem as PersonInformation;
            if (selectedRow != null)
            {
                int selectedId = selectedRow.Id;
                this.RowDoubleClick?.Invoke(this, selectedId);
            }
        }
    }
}

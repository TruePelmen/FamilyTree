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
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for AddTree.xaml
    /// </summary>
    public partial class AddTree : Window
    {
        private readonly ITreeService treeService;
        private readonly IPersonService personService;
        private readonly ITreePersonService treePersonService;
        private int primaryPersonId;
        private string primaryPerson;
        private AddPersonWindow addPersonWindow;

        public AddTree(IPersonService personService, ITreeService treeService, ITreePersonService treePersonService)
        {
            this.InitializeComponent();
            this.personService = personService;
            this.treeService = treeService;
            this.treePersonService = treePersonService;
        }

        public event EventHandler TreeCreated;

        public int TreeId
        { get; set; }

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

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            this.addPersonWindow = DependencyContainer.ServiceProvider.GetRequiredService<AddPersonWindow>();
            this.addPersonWindow.AddNewPerson += this.AddPersonWindowPersonAdded;
            this.addPersonWindow.ShowDialog();
        }

        private void AddPersonWindowPersonAdded(object sender, EventArgs e)
        {
            this.primaryPersonId = this.addPersonWindow.IdNewPerson;
            this.primaryPerson = this.personService.GetPersonById(this.primaryPersonId).FullName;
            this.addPersonButton.Visibility = Visibility.Hidden;
            this.primaryPersonTextBlock.Text = this.primaryPerson;
            this.primaryPersonPanel.Visibility = Visibility.Visible;
        }

        private void CreateTreeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.primaryPersonId == 0)
            {
                MessageBox.Show("Додайте хоча б одну особу перед створенням дерева.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка чи поле з назвою дерева не пусте
            if (string.IsNullOrWhiteSpace(this.treeNameTextBox.Text))
            {
                MessageBox.Show("Введіть назву дерева.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Створення дерева роду
            this.TreeId = this.treeService.AddTree(this.treeNameTextBox.Text, this.primaryPersonId);
            this.treePersonService.AddTreePerson(this.TreeId, this.primaryPersonId);

            MessageBox.Show($"Дерево роду '{this.treeNameTextBox.Text}' було успішно створено з основною особою {this.primaryPersonId}.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            this.TreeCreated?.Invoke(this, EventArgs.Empty);

            // Закриття вікна
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.primaryPersonId != 0)
            {
                this.personService.DeletePerson(this.primaryPersonId);
            }

            this.Close();
        }

        private void DeleteButtonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.primaryPersonPanel.Visibility = Visibility.Hidden;
            this.addPersonButton.Visibility = Visibility.Visible;
            this.personService.DeletePerson(this.primaryPersonId);
            this.primaryPersonId = 0;
        }
    }
}

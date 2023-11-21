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

    /// <summary>
    /// Interaction logic for AddTree.xaml
    /// </summary>
    public partial class AddTree : Window
    {
        private readonly ITreeService treeService;
        private readonly IPersonService personService;

        public AddTree(IPersonService personService, ITreeService treeService)
        {
            this.InitializeComponent();
            this.personService = personService;
            this.treeService = treeService;
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

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            // Відкрити вікно додавання особи
            // Наприклад: var addPersonWindow = new AddPersonWindow();
            // addPersonWindow.Show();
        }

        private void CreateTreeButton_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка чи додана особа
            var allPeople = this.personService.GetAllPeople();
            if (allPeople == null || !allPeople.Any())
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

            // Отримання ідентифікатора основної особи (це лише приклад, вам слід вибрати відповідний ідентифікатор)
            var primaryPersonId = allPeople.FirstOrDefault()?.Id ?? 0;

            // Створення дерева роду
            var treeId = this.treeService.AddTree(this.treeNameTextBox.Text, primaryPersonId);

            MessageBox.Show($"Дерево роду '{this.treeNameTextBox.Text}' було успішно створено з основною особою {primaryPersonId}.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);

            // Закриття вікна
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Закриття вікна
            this.Close();
        }
    }
}

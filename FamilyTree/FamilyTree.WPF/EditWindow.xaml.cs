namespace FamilyTree.WPF
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for EditWindow.xaml.
    /// </summary>
    public partial class EditWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditWindow"/> class.
        /// </summary>
        public EditWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets id focus person.
        /// </summary>
        public int Id { get; set; }
    }
}

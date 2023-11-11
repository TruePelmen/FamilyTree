namespace FamilyTree.WPF
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for ProfileWindow.xaml.
    /// </summary>
    public partial class ProfileWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileWindow"/> class.
        /// </summary>
        public ProfileWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets focus person id.
        /// </summary>
        public int Id { get; set; }
    }
}

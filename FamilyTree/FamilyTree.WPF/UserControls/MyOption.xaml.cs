namespace FamilyTree.WPF.UserControls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Custom user control "MyOption" for displaying a choice option.
    /// </summary>
    public partial class MyOption : UserControl
    {
        /// <summary>
        /// Property to set or get the text content of this element.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyOption));

        /// <summary>
        /// Property to set or get the value indicating the selection state of this element.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(MyOption));

        /// <summary>
        /// Property to set or get the FontAwesome icon that will be displayed next to the text of this element.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(MyOption));

        /// <summary>
        /// Initializes a new instance of the <see cref="MyOption"/> class.
        /// </summary>
        public MyOption()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the text content of this element.
        /// </summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the FontAwesome icon that will be displayed next to the text of this element.
        /// </summary>
        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this option is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }
    }
}

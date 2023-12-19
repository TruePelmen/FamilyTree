namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

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
        /// Gets or sets a value indicating whether this option is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }

        public string Path
        {
            set
            {
                if (value == "Male")
                {
                    this.icon.Source = new BitmapImage(new Uri("C:\\Users\\olesy\\OneDrive\\Документи\\GitHub\\ProgramEngineeringProject-\\FamilyTree\\FamilyTree.WPF\\Images\\man (1).png"));
                }
                else
                {
                    this.icon.Source = new BitmapImage(new Uri("C:\\Users\\olesy\\OneDrive\\Документи\\GitHub\\ProgramEngineeringProject-\\FamilyTree\\FamilyTree.WPF\\Images\\woman (1).png"));
                }
            }
        }
    }
}

namespace FamilyTree.WPF.UserControls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Represents a custom text box control with additional features.
    /// </summary>
    public partial class MyTextBox : UserControl
    {
        /// <summary>
        /// Identifies the Hint dependency property. This property is used to set a hint or placeholder text in the text box.
        /// </summary>
        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(MyTextBox));

        /// <summary>
        /// Initializes a new instance of the <see cref="MyTextBox"/> class.
        /// </summary>
        public MyTextBox()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the hint or placeholder text displayed in the text box.
        /// </summary>
        public string Hint
        {
            get { return (string)this.GetValue(HintProperty); }
            set { this.SetValue(HintProperty, value); }
        }

        /// <summary>
        /// Gets the text entered in the text box.
        /// </summary>
        public string Text
        {
            get { return this.textBox.Text; }
        }

        /// <summary>
        /// Sets the text box border color to indicate an error state.
        /// </summary>
        public void ErrorBorder()
        {
            this.textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        /// <summary>
        /// Sets the text box border color to its normal state.
        /// </summary>
        public void NormalBorder()
        {
            this.textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
        }

        /// <summary>
        /// Sets the text box border color to indicate a success state.
        /// </summary>
        public void SuccessBorder()
        {
            this.textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }
    }
}

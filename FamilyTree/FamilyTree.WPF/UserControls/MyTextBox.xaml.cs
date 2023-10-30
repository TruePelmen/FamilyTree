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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FamilyTree.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for MyTextBox.xaml
    /// </summary>
    public partial class MyTextBox : UserControl
    {
        public MyTextBox()
        {
            InitializeComponent();
        }
        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }
        public string Text
        {
            get { return textBox.Text; }
        }
        public event TextChangedEventHandler TextChanged;
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }
        public void ErrorBorder()
        {
            textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }
        public void NormalBorder()
        {
            textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(238, 240, 232));
        }
        public void SuccessBorder()
        {
            textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }

        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(MyTextBox));

    }
}

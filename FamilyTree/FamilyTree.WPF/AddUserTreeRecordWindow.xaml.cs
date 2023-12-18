namespace FamilyTree.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.WPF.ViewModel;
    using Serilog;

    public partial class AddUserTreeRecordWindow : Window
    {
        public readonly AddUserTreeRecordViewModel ViewModel;

        public AddUserTreeRecordWindow(IUserTreeService userTreeService)
        {
            this.InitializeComponent();
            this.ViewModel = new AddUserTreeRecordViewModel(userTreeService);
            this.DataContext = this.ViewModel;
            this.ViewModel.SuccessfulAdditionRecord += this.ViewModel_SuccessfulAdditionRecord;
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

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.SaveCommand.Execute(null);
        }

        private void ViewModel_SuccessfulAdditionRecord(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

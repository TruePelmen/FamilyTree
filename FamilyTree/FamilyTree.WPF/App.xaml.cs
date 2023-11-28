namespace FamilyTree.WPF
{
    using System.Windows;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DependencyContainer.Initialize();
            LoginWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            //EditUserWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<EditUserWindow>();
            //loginWindow.Show();
            //AddTree addtree = DependencyContainer.ServiceProvider.GetService<AddTree>();
            //addtree.Show();
            EditUserWindow editUserWindow = DependencyContainer.ServiceProvider.GetService<EditUserWindow>();
            editUserWindow.Show();
        }
    }
}

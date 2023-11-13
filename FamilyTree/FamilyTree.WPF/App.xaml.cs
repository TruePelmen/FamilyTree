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
            //LoginWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<LoginWindow>();
            //loginWindow.Show();
            ProfileWindow profile = DependencyContainer.ServiceProvider.GetRequiredService<ProfileWindow>();
            profile.Id = 7;
            profile.Show();
        }
    }
}

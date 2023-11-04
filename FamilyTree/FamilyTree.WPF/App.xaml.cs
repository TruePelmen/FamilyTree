// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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

            // LoginWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<LoginWindow>();
            // loginWindow.Show();
            MainWindow mainWindow = DependencyContainer.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}

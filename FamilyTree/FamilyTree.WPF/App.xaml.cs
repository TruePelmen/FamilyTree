﻿namespace FamilyTree.WPF
{
    using System.Windows;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Events;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();
            Log.Information("The app started its work");

            base.OnStartup(e);
            DependencyContainer.Initialize();
            LoginWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Log.CloseAndFlush();
        }
    }
}

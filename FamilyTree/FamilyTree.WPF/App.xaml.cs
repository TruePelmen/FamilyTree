namespace FamilyTree.WPF
{
    using System.Windows;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;

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
            .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Information("The app started its work");

            //ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            //        .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
            //        .WriteTo.Debug();
            //    builder.AddSerilog(loggerConfiguration.CreateLogger());
            //});
            //ILogger<AddSpecialRecord> logger = loggerFactory.CreateLogger<AddSpecialRecord>();

            base.OnStartup(e);
            DependencyContainer.Initialize();
            LoginWindow loginWindow = DependencyContainer.ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Log.CloseAndFlush();
        }
    }
}

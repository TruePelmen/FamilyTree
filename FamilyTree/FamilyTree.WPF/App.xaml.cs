using FamilyTree.BLL.Interfeces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FamilyTree.DAL.Repositories;
using FamilyTree.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;



namespace FamilyTree.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DependencyContainer.Initialize();
            var userService = DependencyContainer.ServiceProvider.GetRequiredService<IUserService>();
            LoginWindow loginWindow = new LoginWindow(userService);
            loginWindow.Show();

        }
    }
}

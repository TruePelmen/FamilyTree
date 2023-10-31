using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using FamilyTree.DAL.Interfaces;
using FamilyTree.DAL.Repositories;
using FamilyTree.BLL.Interfeces;
using FamilyTree.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;

namespace FamilyTree.WPF
{
    public static class DependencyContainer
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public static void Initialize()
        {
            var services = new ServiceCollection();

            services.AddTransient<IUserService, UserService>(); 
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }

}

using FamilyTree.DAL.Repositories;
using FamilyTree.BLL.Interfeces;
using FamilyTree.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using FamilyTree.BLL.Interfaces;
using Microsoft.Extensions.Logging;

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

            services.AddTransient<ITreeService, TreeService>();
            services.AddTransient<IGenericRepository<Tree>, GenericRepository<Tree>>();

            services.AddTransient<IUserTreeService, UserTreeService>();
            services.AddTransient<IGenericRepository<UserTree>, GenericRepository<UserTree>>();

            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IGenericRepository<Person>, GenericRepository<Person>>();

            services.AddTransient<ITreePersonService, TreePersonService>();
            services.AddTransient<IGenericRepository<TreePerson>, GenericRepository<TreePerson>>();

            services.AddSingleton<LoginWindow>();
            services.AddSingleton<RegistrationWindow>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }

}

namespace FamilyTree.WPF
{
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.BLL.Interfeces;
    using FamilyTree.BLL.Services;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;
    using FamilyTree.DAL.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// A static class responsible for managing the service provider and its initialization.
    /// </summary>
    public static class DependencyContainer
    {
        /// <summary>
        /// Gets the service provider instance for dependency injection.
        /// </summary>
        public static ServiceProvider ServiceProvider { get; private set; }

        public static ILoggerFactory LoggerFactory;
        /// <summary>
        /// Initializes the service provider and registers services.
        /// </summary>
        public static void Initialize()
        {
            var services = new ServiceCollection();

            // Register user-related services and repositories.
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();

            // Register tree-related services and repositories.
            services.AddTransient<ITreeService, TreeService>();
            services.AddTransient<IGenericRepository<Tree>, GenericRepository<Tree>>();

            // Register user-tree-related services and repositories.
            services.AddTransient<IUserTreeService, UserTreeService>();
            services.AddTransient<IUserTreeRepository, UserTreeRepository>();

            // Register person-related services and repositories.
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IPersonRepository, PersonRepositories>();

            // Register tree-person-related services and repositories.
            services.AddTransient<ITreePersonRepository, TreePersonRepository>();
            services.AddTransient<ITreePersonService, TreePersonService>();

            // Register media-related services and repositories.
            services.AddTransient<IMediaPersonService, MediaPersonService>();
            services.AddTransient<IMediaPersonRepository, MediaPersonRepositoty>();

            // Register event-related services and repositories.
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEventRepository, EventRepository>();

            // Register relationship-related services and repositories.
            services.AddTransient<IRelationshipService, RelationshipService>();
            services.AddTransient<IRelationshipRepository, RelationshipRepository>();

            services.AddTransient<IMediaEventRepository, MediaEventRepository>();
            services.AddTransient<IMediaEventService, MediaEventService>();

            services.AddTransient<IGenericRepository<Media>, GenericRepository<Media>>();
            services.AddTransient<IMediaService, MediaService>();

            services.AddTransient<ISpecialRecordRepository, SpecialRecordRepository>();
            services.AddTransient<ISpecialRecordService, SpecialRecordService>();


            // Register the application windows and user controls.
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<RegistrationWindow>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<EditWindow>();
            services.AddTransient<AddEvent>();
            services.AddTransient<ProfileWindow>();
            services.AddTransient<PhotoWindow>();
            services.AddTransient<EventDetails>();
            services.AddTransient<UserControls.Tree>();
            services.AddTransient<AddSpecialRecord>();
            services.AddTransient<AddTree>();
            services.AddTransient<EditUserWindow>();

            // Logger
            services.AddSingleton<ILogger<LoginWindow>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<LoginWindow>();
            });

            services.AddSingleton<ILogger<RegistrationWindow>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<RegistrationWindow>();
            });

            services.AddSingleton<ILogger<MainWindow>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<MainWindow>();
            });

            services.AddTransient<ILogger<EditWindow>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<EditWindow>();
            });

            services.AddTransient<ILogger<AddEvent>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<AddEvent>();
            });

            services.AddTransient<ILogger<ProfileWindow>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<ProfileWindow>();
            });

            services.AddTransient<ILogger<PhotoWindow>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<PhotoWindow>();
            });            
            
            services.AddSingleton<ILogger<AddSpecialRecord>>(serviceProvider =>
            {
                return LoggerFactory.CreateLogger<AddSpecialRecord>();
            });

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}

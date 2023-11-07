﻿// <copyright file="DependencyContainer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FamilyTree.WPF
{
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.BLL.Interfeces;
    using FamilyTree.BLL.Services;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;
    using FamilyTree.DAL.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A static class responsible for managing the service provider and its initialization.
    /// </summary>
    public static class DependencyContainer
    {
        /// <summary>
        /// Gets the service provider instance for dependency injection.
        /// </summary>
        public static ServiceProvider ServiceProvider { get; private set; }

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

            // Register the application windows and user controls.
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<RegistrationWindow>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<EditWindow>();
            services.AddTransient<UserControls.Tree>();
            services.AddTransient<AddEvent>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}

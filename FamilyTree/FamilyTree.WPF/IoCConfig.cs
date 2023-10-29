using Autofac;
using FamilyTree.BLL.Interfeces;
using FamilyTree.BLL.Services;
using System;

namespace FamilyTree.WPF
{
    public class IoCConfig
    {
        public IContainer Configure()
        {
            var builder = new ContainerBuilder();

            
            builder.RegisterType<UserService>().As<IUserService>();


            return builder.Build();
        }
    }

}


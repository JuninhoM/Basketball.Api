using Autofac;
using Basketball.Infra.Helpers.Autofac;
using Basketball.Repository.Repositories;
using System.Reflection;

namespace Basketball.Service.Services
{
    public class ServiceRegisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryRegisterModule());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterAssemblyComponents(Assembly.GetExecutingAssembly());

        }
    }
}

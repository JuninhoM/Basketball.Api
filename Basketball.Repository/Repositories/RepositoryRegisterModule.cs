using Autofac;
using Basketball.Infra.Helpers.Autofac;
using System.Reflection;

namespace Basketball.Repository.Repositories
{
    public class RepositoryRegisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().AsImplementedInterfaces()
               .InstancePerLifetimeScope()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterAssemblyComponents(Assembly.GetExecutingAssembly());
        }
    }
}

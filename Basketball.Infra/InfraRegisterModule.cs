using Autofac;
using Basketball.Infra.Helpers.Autofac;
using System.Reflection;

namespace Basketball.Infra
{
    public class InfraRegisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyComponents(Assembly.GetExecutingAssembly());
        }
    }
}

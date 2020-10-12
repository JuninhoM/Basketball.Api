using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Basketball.Infra.Helpers.Autofac
{
    /// <summary>
    /// Provides automatic component registration by scanning assemblies and types for
    /// those that have the <see cref="ComponentAttribute"/> annotation.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
            /// Registers the components found in the given assemblies.
            /// </summary>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAssemblyComponents(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            return RegisterComponents(builder, assemblies.SelectMany(x => x.GetExportedTypes()));
        }

        /// <summary>
            /// Registers the components found in the given set of types.
            /// </summary>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterComponents(this ContainerBuilder builder, IEnumerable<Type> types)
        {
            return builder.RegisterTypes(types.Where(t => t.GetCustomAttributes(typeof(ComponentAttribute), true).Any()).ToArray())
            .As(t => t.GetCustomAttributes(typeof(ComponentAttribute), true).OfType<ComponentAttribute>().First().RegisterAs ?? t).InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        /// <summary>
            /// Registers the components found in the given set of types.
            /// </summary>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterComponents(this ContainerBuilder builder, params Type[] types)
        {
            return RegisterComponents(builder, (IEnumerable<Type>)types);
        }
    }
}

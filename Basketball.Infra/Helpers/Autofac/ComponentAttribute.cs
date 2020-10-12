using System;
using System.Collections.Generic;
using System.Text;

namespace Basketball.Infra.Helpers.Autofac
{
    /// <summary>
    /// Marks the decorated class as a component that will be available from
    /// the service locator / component container.
    /// [Component(typeof(INegociacaoCombustivelService))]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
            /// Initializes a new instance of the <see cref="ComponentAttribute"/> class,
            /// marking the decorated class as a component that will be available from
            /// the service locator / component container, registered with all
            /// implemented interfaces as well as the concrete type.
            /// </summary>
        public ComponentAttribute()
        {
        }

        /// <summary>
            /// Initializes a new instance of the <see cref="ComponentAttribute"/> class,
            /// marking the decorated class as a component that will be available from
            /// the service locator / component container using the specified
            /// <paramref name="registerAs"/> type.
            /// </summary>
            /// <param name="registerAs">The type to use to register the decorated component.</param>
        public ComponentAttribute(Type registerAs)
        {
            this.RegisterAs = registerAs;
        }

        /// <summary>
            /// Type to use for the component registration.
            /// </summary>
        public Type RegisterAs { get; private set; }
    }
}

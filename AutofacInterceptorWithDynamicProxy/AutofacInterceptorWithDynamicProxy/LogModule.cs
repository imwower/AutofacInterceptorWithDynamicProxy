using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace AutofacInterceptorWithDynamicProxy
{
    public class LogModule : Autofac.Module
    {
        private static Type @interface = typeof(ILogger);

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DefaultLogFactory>()
                .PreserveExistingDefaults()
                .SingleInstance();

            builder.Register(CreateLogger);
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            var instanceType = registration.Activator.LimitType;
            var propertyInfo = instanceType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.PropertyType == @interface);
            if (propertyInfo == null)
                return;
            registration.Preparing += (o, e) =>
            {
                //add current implemetation type to parameters:
                e.Parameters = e.Parameters.Union(new[] { new TypedParameter(typeof(Type), instanceType) });
            };
        }

        private static ILogger CreateLogger(IComponentContext context, IEnumerable<Parameter> parameters)
        {
            //here is the magic:
            var instanceType = parameters.TypedAs<Type>();
            var factory = context.Resolve<DefaultLogFactory>();
            var logger = factory.CreateLogger(instanceType);
            return logger;
        }
    }
}

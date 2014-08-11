using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace AutofacInterceptorWithDynamicProxy
{
    public static class AutofacExtensions
    {
        /// <summary>
        /// Special autowiring mode that also scans Resolve parameters while wiring properties
        /// </summary>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> PropertiesAutowiredWithParameters<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> rb)
        {
            AutowiringPropertyInjectorWithParameters injector = new AutowiringPropertyInjectorWithParameters();
            rb.RegistrationData.ActivatingHandlers.Add(delegate(object s, ActivatingEventArgs<object> e)
            {
                injector.InjectProperties(e.Context, e.Instance, e.Parameters);
            });
            return rb;
        }

        /// <summary>
        /// Special autowiring mode that also scans Resolve parameters while wiring properties
        /// </summary>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> PropertiesAutowiredWithParameters<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> rb, bool allowCircularDependencies)
        {
            if (allowCircularDependencies)
            {
                AutowiringPropertyInjectorWithParameters injector = new AutowiringPropertyInjectorWithParameters();
                rb.RegistrationData.ActivatedHandlers.Add(delegate(object s, ActivatedEventArgs<object> e)
                {
                    injector.InjectProperties(e.Context, e.Instance, e.Parameters);
                });
                return rb;
            }
            return rb.PropertiesAutowired();
        }
    }

    internal class AutowiringPropertyInjectorWithParameters
    {
        public void InjectProperties(IComponentContext context, object instance, IEnumerable<Parameter> parameters)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            foreach (PropertyInfo info in instance.GetType().GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance))
            {
                Type propertyType = info.PropertyType;
                if (!propertyType.IsValueType && info.GetIndexParameters().Length == 0)
                {
                    MethodInfo[] accessors = info.GetAccessors(false);
                    if (accessors.Length != 1 || accessors[0].ReturnType == typeof(void))
                    {
                        if (context.IsRegistered(propertyType))
                        {
                            object obj2 = context.Resolve(propertyType, parameters);
                            info.SetValue(instance, obj2, null);
                        }
                    }
                }
            }
        }
    }
}

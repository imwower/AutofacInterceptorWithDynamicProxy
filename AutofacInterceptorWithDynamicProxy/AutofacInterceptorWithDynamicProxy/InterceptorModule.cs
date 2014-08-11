using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Extras.DynamicProxy2;

namespace AutofacInterceptorWithDynamicProxy
{
    public class InterceptorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //override existing registations:
            builder.RegisterType<TestImplemetation>()
                .As<ITestInterface>()
                .PropertiesAutowiredWithParameters()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(DefaultInterceptor));
            //register interceptor:
            builder.RegisterType<DefaultInterceptor>()
                //the PropertiesAutowiredWithParameters method is necessary,
                //because the default "PropertiesAutowired" method will miss the parameters:
                .PropertiesAutowiredWithParameters()
                .SingleInstance();
        }
    }
}

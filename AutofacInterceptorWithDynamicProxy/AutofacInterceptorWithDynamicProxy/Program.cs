using System;
using Autofac;

namespace AutofacInterceptorWithDynamicProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new LogModule());

            builder.RegisterType<TestImplemetation>()
                .As<ITestInterface>()
                .PreserveExistingDefaults()
                .PropertiesAutowiredWithParameters();

            //comment this line to disable interceptor:
            builder.RegisterModule(new InterceptorModule());

            var container = builder.Build();

            var instance = container.Resolve<ITestInterface>();
            Console.WriteLine(instance.Hello());

            Console.ReadLine();
        }
    }
}

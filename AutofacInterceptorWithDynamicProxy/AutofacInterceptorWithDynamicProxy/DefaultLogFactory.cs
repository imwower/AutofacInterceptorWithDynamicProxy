using System;
using log4net;

namespace AutofacInterceptorWithDynamicProxy
{
    public class DefaultLogFactory
    {
        static DefaultLogFactory()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public ILogger CreateLogger(Type type)
        {
            return new DefaultLogger(LogManager.GetLogger(type));
        }
    }
}

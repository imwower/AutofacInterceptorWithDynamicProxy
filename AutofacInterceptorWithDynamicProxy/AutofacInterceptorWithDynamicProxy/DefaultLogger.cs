using System;
using log4net;

namespace AutofacInterceptorWithDynamicProxy
{
    public class DefaultLogger : ILogger
    {
        private ILog logger;

        public DefaultLogger(ILog logger)
        {
            this.logger = logger;
        }

        public void Debug(string message, params object[] objs)
        {
            if (this.logger.IsDebugEnabled)
                this.logger.DebugFormat(message, objs);
        }

        public void Error(string message, Exception exception)
        {
            if (this.logger.IsErrorEnabled)
                this.logger.Error(message, exception);
        }

        public void Info(string message, params object[] objs)
        {
            if (this.logger.IsInfoEnabled)
                this.logger.InfoFormat(message, objs);
        }

        public void Warn(string message, params object[] objs)
        {
            if (this.logger.IsWarnEnabled)
                this.logger.WarnFormat(message, objs);
        }
    }
}

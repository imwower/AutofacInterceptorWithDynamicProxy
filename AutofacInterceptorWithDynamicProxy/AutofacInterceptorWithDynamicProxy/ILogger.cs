using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutofacInterceptorWithDynamicProxy
{
    public interface ILogger
    {
        void Debug(string message, params object[] objs);
        void Info(string message, params object[] objs);
        void Warn(string message, params object[] objs);
        void Error(string message, Exception exception);
    }
}

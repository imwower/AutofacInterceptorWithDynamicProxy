using Castle.DynamicProxy;

namespace AutofacInterceptorWithDynamicProxy
{
    public class DefaultInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        public void Intercept(IInvocation invocation)
        {
            Logger.Debug("method before");
            invocation.Proceed();
            Logger.Debug("method after");
        }
    }
}

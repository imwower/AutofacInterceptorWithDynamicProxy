
namespace AutofacInterceptorWithDynamicProxy
{
    public class TestImplemetation: ITestInterface
    {
        public ILogger Logger { get; set; }

        public string Hello()
        {
            Logger.Debug("hello world");
            return "hello";
        }
    }
}

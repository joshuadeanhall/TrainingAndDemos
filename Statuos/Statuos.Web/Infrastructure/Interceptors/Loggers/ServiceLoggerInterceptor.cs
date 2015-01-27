using Castle.Core.Logging;
using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Text;
using System.Web;


namespace Statuos.Web.Infrastructure.Interceptors.Loggers
{
    /// <summary>
    /// Serves as the AOP component for logging the service layer.
    /// </summary>
    public class ServiceLoggerInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        public static String CreateInvocationLogString(IInvocation invocation)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendFormat("User: {0} Called: {1}.{2} at {3}(", HttpContext.Current.User.Identity.Name, invocation.TargetType.Name, invocation.Method.Name, DateTime.Now);
            foreach (object argument in invocation.Arguments)
            {
                String argumentDescription = argument == null ? "null" : DumpObject(argument);
                sb.Append(argumentDescription).Append(",");
            }
            if (invocation.Arguments.Count() > 0) sb.Length--;
            sb.Append(")");
            return sb.ToString();
        }

        private static string DumpObject(object argument)
        {
            Type objtype = argument.GetType();
            if (objtype == typeof(String) || objtype.IsPrimitive || !objtype.IsClass)
                return objtype.ToString();

            return objtype.ToString();
            //return DataContractSerialize(argument, objtype);
        }
        //TODO This currently does not work with the dynamic proxies.
        //private static string DataContractSerialize(object argument, Type argumentType)
        //{

        //    MemoryStream memStm = new MemoryStream();
        //    var serializer = new DataContractSerializer(argumentType);
        //    serializer.WriteObject(memStm, argument);

        //    memStm.Seek(0, SeekOrigin.Begin);
        //    string result = new StreamReader(memStm).ReadToEnd();
        //    return result;

        //}
        public void Intercept(IInvocation invocation)
        {
            if (Logger.IsDebugEnabled) Logger.Debug(CreateInvocationLogString(invocation));
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                if (Logger.IsErrorEnabled) Logger.Error(CreateInvocationLogString(invocation), ex);
                throw;
            }
        }
    }
}
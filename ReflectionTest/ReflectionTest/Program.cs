using System;
using System.Diagnostics;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new ServiceProvider();

            var clientOne = new ClientOne(provider);

            var clientTwo = new ClienTwo(provider);

            provider.WriteOut("Message from Main Program");

            for (int i = 0; i < 1000; i++)
            {

                clientOne.MethodOne();

                clientTwo.MethodTwo();
                clientTwo.MethodOne();

            }

            provider.WriteOut("End of the program");

            Console.ReadLine();
        }
    }

    public class ServiceProvider
    {
        public void WriteOut(string message)
        {
            var callerInfo = GetCallerInfo();
            Console.WriteLine($"{callerInfo[0]} {callerInfo[1]} {message}");
        }

        private string GetCallerMethodName()
        {
            StackTrace stackTrace = new StackTrace();

            var methodName = stackTrace.GetFrame(2).GetMethod().Name;

            return methodName;
        }

        private string GetCallerClassName()
        {
            StackTrace stackTrace = new StackTrace();

            var className = stackTrace.GetFrame(2).GetMethod().DeclaringType.Name;

            return className;
        }

        private string[] GetCallerInfo()
        {
            string[] callerInfo = new string[2];
            StackTrace stackTrace = new StackTrace();

            callerInfo[0] = stackTrace.GetFrame(2).GetMethod().DeclaringType.Name;
            callerInfo[1] = stackTrace.GetFrame(2).GetMethod().Name;

            return callerInfo;
        }
    }

    public class ClientOne
    {
        private readonly ServiceProvider _serviceProvider;

        public ClientOne(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void MethodOne()
        {
            _serviceProvider.WriteOut("ClientOne Message");
        }
    }

    public class ClienTwo
    {
        private readonly ServiceProvider _serviceProvider;

        public ClienTwo(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void MethodOne()
        {
            _serviceProvider.WriteOut("ClientTwo Message from MethodOne");
        }

        public void MethodTwo()
        {
            _serviceProvider.WriteOut("ClientTwo Message from MethodTwo");
        }
    }
}

using Topshelf;

namespace SimpleServices.Web
{
    using System;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string arg0 = string.Empty;
            if (args.Length > 0)
                arg0 = (args[0] ?? string.Empty).ToLower();

            if (arg0 == "-fake")
            {
                RunFake();
            }
            else
            {
                Run();
            }
        }

        public static void Run()
        {
            HostFactory.Run(x =>
            {
                x.Service<SimpleServiceWebService>(s =>
                {
                    s.ConstructUsing(name => new SimpleServiceWebService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Web Simple Service");
                x.SetDisplayName("NancyWebSimple");
                x.SetServiceName("NancyWebSimple");
            });
        }

        public static void RunFake()
        {
            var service = new SimpleServiceWebService();
            service.Start();

            Console.ReadLine();
        }
    }
}

using Topshelf;

namespace ReportService
{
    class Program
    {
        static void Main(string[] args)
        {
            string arg0 = string.Empty;
            if (args.Length > 0)
                arg0 = (args[0] ?? string.Empty).ToLower();

            if (arg0 == "-fake")
            {
                var service = new ReportService();
                service.Start();
            }
            else
            {
                HostFactory.Run(x =>
                {
                    x.Service<ReportService>(s =>
                    {
                        s.ConstructUsing(name => new ReportService());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsLocalSystem();

                    x.SetDescription("Message Service");
                    x.SetDisplayName("MessageService");
                    x.SetServiceName("MessageService");
                    x.StartAutomatically();
                });
            }
        }
    }
}

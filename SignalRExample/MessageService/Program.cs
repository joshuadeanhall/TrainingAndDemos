using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Topshelf;

namespace MessageService
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
                SignalRService service = new SignalRService();
                service.Start();
            }
            else
            {
                HostFactory.Run(x =>
                {
                    x.Service<SignalRService>(s =>
                    {
                        s.ConstructUsing(name => new SignalRService());
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

using System;
using System.Threading;
using Messages;
using Rebus;

namespace ReportService.Handlers
{
    public class CreateReportMessgeHandler : IHandleMessages<CreateReportMessage>
    {
        public IBus Bus { get; set; }
        public void Handle(CreateReportMessage message)
        {
            var rand = new Random();
            Thread.Sleep(rand.Next(5000, 15000));
            Bus.Send(new NotificationMessage
            {
                Message = string.Format("Report {0}", message.ReportName),
                Title = "Report finished",
                UserName = message.UserName
            });
        }
    }
}

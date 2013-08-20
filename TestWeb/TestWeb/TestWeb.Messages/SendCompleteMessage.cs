using NServiceBus;

namespace TestWeb.Messages
{
    public class SendCompleteMessage : ICommand
    {
        public string Name { get; set; }
    }
}
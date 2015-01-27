using System;
using NServiceBus;

namespace TestWeb.Messages
{
    public class SendCommandMessage : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
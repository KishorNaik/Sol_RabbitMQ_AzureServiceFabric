using MassTransit;
using Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Api.Messages
{
    public class SendDemoConsumer : IConsumer<SendDemoRequest>
    {
        Task IConsumer<SendDemoRequest>.Consume(ConsumeContext<SendDemoRequest> context)
        {
            Debug.Write($"Message : {context.Message.Message}");
            return Task.CompletedTask;
        }
    }
}
using MassTransit;
using Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Api.Messages
{
    public class PublishDemoConsumer : IConsumer<PublishDemoRequest>
    {
        Task IConsumer<PublishDemoRequest>.Consume(ConsumeContext<PublishDemoRequest> context)
        {
            Debug.WriteLine($"Message: {context.Message.Message}");
            return Task.CompletedTask;
        }
    }
}
using MassTransit;
using Shared.Message.Requests;
using Shared.Message.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Api.Messages
{
    public class RequestResponseDemoConsumer : IConsumer<RequestDemoModel>
    {
        public async Task Consume(ConsumeContext<RequestDemoModel> context)
        {
            Debug.WriteLine($"Id:{context.Message.Id}");

            // Send Dummy Record
            var responseDemoModel = new ResponseDemoModel()
            {
                Id = context.Message.Id,
                FullName = "Kishor Naik"
            };

            await context.RespondAsync<ResponseDemoModel>(responseDemoModel);
        }
    }
}
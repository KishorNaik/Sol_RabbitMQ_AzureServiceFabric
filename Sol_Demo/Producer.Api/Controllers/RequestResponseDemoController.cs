using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Message.Requests;
using Shared.Message.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.Api.Controllers
{
    [Route("api/request-response")]
    [ApiController]
    public class RequestResponseDemoController : ControllerBase
    {
        private readonly IBus bus = null;

        public RequestResponseDemoController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpPost("demo")]
        public async Task<IActionResult> Demo([FromBody] RequestDemoModel requestDemoModel)
        {
            var client = bus.CreateRequestClient<RequestDemoModel>(new Uri("queue:demo-request-response-queue"));

            var response = await client.GetResponse<ResponseDemoModel>(requestDemoModel);

            return base.Ok(response.Message);
        }
    }
}
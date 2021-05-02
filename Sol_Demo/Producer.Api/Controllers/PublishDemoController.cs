using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.Api.Controllers
{
    [Route("api/publish")]
    [ApiController]
    public class PublishDemoController : ControllerBase
    {
        private readonly IBus bus = null;

        public PublishDemoController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpPost("demo")]
        public async Task<IActionResult> Demo([FromBody] PublishDemoRequest publishDemoRequest)
        {
            try
            {
                await bus.Publish<PublishDemoRequest>(publishDemoRequest);
                return base.Ok();
            }
            catch
            {
                throw;
            }
        }
    }
}
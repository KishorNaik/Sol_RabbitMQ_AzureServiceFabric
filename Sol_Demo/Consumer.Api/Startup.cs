using Consumer.Api.Messages;
using Framework.RabbitMQ.Extension;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMQService(
                "rabbitmq://localhost",
                "guest",
                "guest",
                addConsumer: (config) =>
                {
                    config.AddConsumer<PublishDemoConsumer>();
                    config.AddConsumer<SendDemoConsumer>();
                    config.AddConsumer<RequestResponseDemoConsumer>();
                },
                receiveEndPoints: (endPoint, busFactory) =>
                {
                    endPoint.ReceiveEndpoint("demo-publish-event", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<PublishDemoConsumer>(busFactory);
                    });

                    endPoint.ReceiveEndpoint("demo-send-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<SendDemoConsumer>(busFactory);
                    });

                    endPoint.ReceiveEndpoint("demo-request-response-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<RequestResponseDemoConsumer>(busFactory);
                    });
                }
                );

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Consumer.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consumer.Api v1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
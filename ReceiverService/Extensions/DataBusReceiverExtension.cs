using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Extensions
{
    public static class DataBusReceiverExtension
    {
        public static IServiceCollection RegisterDataReceiverServices(this IServiceCollection services, IConfiguration section)
        {
            services.AddMassTransit(c =>
            {
                c.AddConsumer<DataBusConsumer>();
            });

            services.AddHostedService<DataBusReceiverWorker>();

            var appSettings = section.GetSection("BusSettings");

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(appSettings["HostName"], appSettings["VirtualHost"],
                    h =>
                    {
                        h.Username(appSettings["UserName"]);
                        h.Password(appSettings["Password"]);
                    });

                //cfg.SetLoggerFactory(provider.GetService<ILoggerFactory>());

            }));

            return services;
        }
    }
}

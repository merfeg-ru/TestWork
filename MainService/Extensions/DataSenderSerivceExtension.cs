﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainService.Extensions
{
    public static class DataSenderSerivceExtension
    {
        public static IServiceCollection RegisterDataSenderServices(this IServiceCollection services, IConfiguration section)
        {
            var appSettings = section.GetSection("BusSettings");

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(appSettings["HostName"], appSettings["VirtualHost"],
                    h => {
                        h.Username(appSettings["UserName"]);
                        h.Password(appSettings["Password"]);
                    });

                cfg.ExchangeType = ExchangeType.Direct;
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IDataSenderService, DataSenderService>();

            return services;
        }
    }
}

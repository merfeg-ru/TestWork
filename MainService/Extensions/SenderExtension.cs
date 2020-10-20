using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using MassTransit;
using MediatR;
using RabbitMQ.Client;

using Sender.Models;
using Sender.Validators;


namespace Sender.Extensions
{
    public static class SenderExtension
    {
        public static IServiceCollection RegisterDataSenderServices(this IServiceCollection services, IConfiguration section)
        {
            // Bus
            var busSettings = section.GetSection("BusSettings");
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(busSettings["HostName"], busSettings["VirtualHost"],
                    h => {
                        h.Username(busSettings["UserName"]);
                        h.Password(busSettings["Password"]);
                    });

                cfg.ExchangeType = ExchangeType.Direct;
            }));
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            // Validator
            services.AddTransient<IValidator<User>, UserValidator>();

            // MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Main service
            services.AddSingleton<ISenderService, SenderService>();

            return services;
        }
    }
}

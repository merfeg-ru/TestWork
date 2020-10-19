using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Receiver.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Receiver.Extensions
{
    public static class ReceiverExtension
    {
        public static IServiceCollection RegisterDataReceiverServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Main
            services.AddScoped<IReceiverService, ReceiverService>();
            services.AddScoped<IReceiverRepository, ReceiverRepository>();

            // Background Service
            services.AddHostedService<ReceiverWorker>();

            // Bus
            services.AddMassTransit(c =>
            {
                c.AddConsumer<Consumer>();
            });

            var busSettings = configuration.GetSection("BusSettings");
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(busSettings["HostName"], busSettings["VirtualHost"],
                    h =>
                    {
                        h.Username(busSettings["UserName"]);
                        h.Password(busSettings["Password"]);
                    });
            }));

            // Data Base
            string connection = configuration.GetConnectionString("UserDataBase");
            services.AddDbContext<UsersContext>(options => options.UseNpgsql(connection));

            //// Controllers
            //services.AddControllersWithViews();

            // MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

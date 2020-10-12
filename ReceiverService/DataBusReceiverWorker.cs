using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiverService
{
    public class DataBusReceiverWorker : BackgroundService
    {
        private readonly ILogger<DataBusReceiverWorker> _logger;
        private readonly IBusControl _busControl;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public DataBusReceiverWorker(IServiceProvider serviceProvider, ILogger<DataBusReceiverWorker> logger, IBusControl busControl, IConfiguration configuration)
        {
            _logger = logger;
            _busControl = busControl;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //=============
                //IDataBusReceiverService.Инициализация репозитория
                //============

                //=============
                //IDataBusReceiverService.Инициализация подписки на шину данных
                //============

                var appSettings = _configuration.GetSection("BusSettings");
                var userChangeHandler = _busControl.ConnectReceiveEndpoint(appSettings["QueueName"], x =>
                {
                    x.Consumer<DataBusConsumer>(_serviceProvider);
                });

                await userChangeHandler.Ready;

                _logger.LogWarning("DataBusReceiverWorker запущен!");
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBusReceiverWorker не запущен.", ex);
            }
        }
    }
}

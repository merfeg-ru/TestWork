using MainService.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MainService
{
    public class DataSenderService : IDataSenderService
    {
        private readonly ILogger<DataSenderService> _logger;
        private readonly IPublishEndpoint _endpoint;


        public DataSenderService(ILogger<DataSenderService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        public async Task Send(User request, CancellationToken cancellationToken)
        {
            try
            {
                var userMessage = new UserMessage()
                {
                    MessageId = Guid.NewGuid(),
                    User = request,
                    CreationDate = DateTime.Now
                };

                await _endpoint.Publish<IUserMessage>(userMessage, cancellationToken);
                _logger.LogWarning($"Передача в шину данных выполнена [{userMessage}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }


}


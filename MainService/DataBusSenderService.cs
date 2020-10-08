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
    public class DataBusSenderService : IDataBusSenderService
    {
        private readonly ILogger<DataBusSenderService> _logger;
        private readonly IPublishEndpoint _endpoint;


        public DataBusSenderService(ILogger<DataBusSenderService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        public async Task<bool> Send(User request, CancellationToken cancellationToken)
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
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }


}


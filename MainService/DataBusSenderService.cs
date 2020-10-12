using SenderService.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommonData;

namespace SenderService
{
    public class DataBusSenderService : IDataBusSenderService
    {
        private readonly ILogger<DataBusSenderService> _logger;
        private readonly IPublishEndpoint _endpoint;


        public DataBusSenderService(ILogger<DataBusSenderService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
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


﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using MassTransit;

using CommonData;
using Sender.Models;

namespace Sender
{
    public class SenderService : ISenderService
    {
        private readonly ILogger<SenderService> _logger;
        private readonly IPublishEndpoint _endpoint;


        public SenderService(ILogger<SenderService> logger, IPublishEndpoint endpoint)
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


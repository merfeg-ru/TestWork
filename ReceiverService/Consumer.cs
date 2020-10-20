using CommonData;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver
{
    public class Consumer : IConsumer<IUserMessage>
    {
        private readonly ILogger<Consumer> _logger;
        private readonly IReceiverService _receiverService;

        public Consumer(ILogger<Consumer> logger, IReceiverService receiverService) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _receiverService = receiverService ?? throw new ArgumentNullException(nameof(receiverService));
        }

        public async Task Consume(ConsumeContext<IUserMessage> context)
        {
            try
            {
                _logger.LogInformation($"Получены данные {context.Message.CreationDate} {context.Message.MessageId}" +
                    $" [{context.Message.User.FirstName}" +
                    $" {context.Message.User.LastName}" +
                    $" {context.Message.User.MiddleName}" +
                    $" {context.Message.User.PhoneNumber}" +
                    $" {context.Message.User.EMail}" +
                    $"]");

                // Запись в БД
                var id = await _receiverService.AddUserAsync(context.Message.User, CancellationToken.None);
                _logger.LogWarning($"Данные записаны c ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }
    }
}

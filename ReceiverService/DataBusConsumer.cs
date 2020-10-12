using CommonData;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService
{
    public class DataBusConsumer : IConsumer<IUserMessage>
    {
        private readonly ILogger<DataBusConsumer> _logger;
        public DataBusConsumer(ILogger<DataBusConsumer> logger) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<IUserMessage> context)
        {
            try
            {
                _logger.LogWarning($"Что-то пришло {context.Message.MessageId} [{context.Message.User.FirstName} {context.Message.User.LastName} {context.Message.User.PhoneNumber} {context.Message.User.EMail}]");

                // =======================
                // IDataBusReceiverService.Запись в БД
                // =======================
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using SenderService.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SenderService.Commands
{
    public class SenderCommandHandler : IRequestHandler<User, bool>
    {
        private readonly ISenderService _dataBusSenderService;
        private readonly ILogger<SenderCommandHandler> _logger;

        public SenderCommandHandler(ILogger<SenderCommandHandler> logger, ISenderService dataBusSenderService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dataBusSenderService = dataBusSenderService ?? throw new ArgumentNullException(nameof(dataBusSenderService));
        }

        public async Task<bool> Handle(User request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Отправка данных [{request}]...");
            bool result = await _dataBusSenderService.Send(request, cancellationToken);
            if (result)
            {
                _logger.LogInformation("Отправка данных завершилась успешно");
            }
            else
            {
                _logger.LogInformation("Ошибка отправки данных");
            }

            return result;
        }
    }
}

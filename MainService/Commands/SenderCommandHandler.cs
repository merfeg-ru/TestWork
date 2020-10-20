using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using MediatR;


namespace Sender.Commands
{
    public class SenderCommandHandler : IRequestHandler<SenderCommand, bool>
    {
        private readonly ISenderService _dataBusSenderService;
        private readonly ILogger<SenderCommandHandler> _logger;

        public SenderCommandHandler(ILogger<SenderCommandHandler> logger, ISenderService dataBusSenderService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dataBusSenderService = dataBusSenderService ?? throw new ArgumentNullException(nameof(dataBusSenderService));
        }

        public async Task<bool> Handle(SenderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Отправка данных [{request.User}]...");
            bool result = await _dataBusSenderService.Send(request.User, cancellationToken);
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

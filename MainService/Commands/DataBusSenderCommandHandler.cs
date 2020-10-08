using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MainService.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MainService.Commands
{
    public class DataBusSenderCommandHandler : IRequestHandler<User, bool>
    {
        private readonly IDataBusSenderService _dataBusSenderService;
        private readonly ILogger<DataBusSenderCommandHandler> _logger;

        public DataBusSenderCommandHandler(ILogger<DataBusSenderCommandHandler> logger, IDataBusSenderService dataBusSenderService)
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

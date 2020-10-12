using CommonData;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService
{
    public class DataBusReceiverService
    {
        private readonly ILogger<DataBusReceiverService> _logger;
        public DataBusReceiverService(ILogger<DataBusReceiverService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}

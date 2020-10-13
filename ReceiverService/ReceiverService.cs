using CommonData;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService
{
    public class ReceiverService : IReceiverService
    {
        private readonly ILogger<ReceiverService> _logger;
        private readonly IReceiverRepository _repository;

        // Признак выполнена ли первичная инициализация репозитория
        private static bool isInitRepository = false;

        public ReceiverService(ILogger<ReceiverService> logger, IReceiverRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            InitializeReceiverRepository();
        }

        public async Task AddUserAsync(IUser user)
        {
            var userDTO = new UserDTO
            {
                EMail = user.EMail,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                PhoneNumber = user.PhoneNumber
            };

            await _repository.AddUserAsync(userDTO);
        }

        public void InitializeReceiverRepository()
        {
            if (!isInitRepository)
            {
                _repository.Initialize();
                isInitRepository = true;
            }
        }
    }
}

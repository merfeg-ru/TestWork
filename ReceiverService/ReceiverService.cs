using CommonData;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AddUserAsync(IUser user)
        {
            var userDTO = new UserDTO
            {
                EMail = user.EMail,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                PhoneNumber = user.PhoneNumber
            };

            return await _repository.AddUserAsync(userDTO);
        }

        public void InitializeReceiverRepository()
        {
            if (!isInitRepository)
            {
                _repository.Initialize();
                isInitRepository = true;
            }
        }

        public int GetUsersPageCount(int pageSize)
        {
            var allUsers = _repository.GetUsers();
            return allUsers.Count() / pageSize;
        }

        public async Task<List<User>> GetUsersPaginationAsync(int organizationId, int pageNumber, int pageSize)
        {
            return await _repository.GetUsers()
                .Where(w => w.Organization.OrganizationId == organizationId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new User
                    {
                        Id = s.UserId,
                        EMail = s.EMail,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        MiddleName = s.MiddleName,
                        PhoneNumber = s.PhoneNumber
                    })
                .ToListAsync();
        }

        public async Task AddUserToOrganizationAsync(int userId, int organizationId)
        {
            await _repository.AddUserToOrganizationAsync(userId, organizationId);
        }
    }
}

using CommonData;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReceiverService.Exceptions;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiverService
{
    public class ReceiverService : IReceiverService
    {
        private const int PAGE_SIZE_DEFAULT = 3;
        private const int PAGE_NUMBER_DEFAULT = 1;

        private readonly IReceiverRepository _repository;

        // Признак выполнена ли первичная инициализация репозитория
        private static bool isInitRepository = false;

        public ReceiverService(IReceiverRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            InitializeReceiverRepository();
        }

        public async Task<int> AddUserAsync(IUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userDTO = new UserDTO
            {
                EMail = user.EMail,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                PhoneNumber = user.PhoneNumber
            };

            return await _repository.AddUserAsync(userDTO, cancellationToken);
        }

        public void InitializeReceiverRepository()
        {
            if (!isInitRepository)
            {
                _repository.Initialize();
                isInitRepository = true;
            }
        }

        public async Task<int> GetUsersPageCountAsync(int organizationId, int pageSize, CancellationToken cancellationToken)
        {
            return await _repository.GetUsers()
                .Where(w => w.Organization.OrganizationId == organizationId)
                .CountAsync(cancellationToken) / ((pageSize < 1) ? PAGE_SIZE_DEFAULT : pageSize);
        }

        public async Task<List<User>> GetUsersPaginationAsync(int organizationId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var validPageSize = (pageSize < 1) ? PAGE_SIZE_DEFAULT : pageSize;
            var validPageNumber = (pageNumber < 1) ? PAGE_NUMBER_DEFAULT : pageSize;

            return await _repository.GetUsers()
                .Where(w => w.Organization.OrganizationId == organizationId)
                .Skip((validPageNumber - 1) * validPageSize)
                .Take(validPageSize)
                .Select(s => new User
                {
                    Id = s.UserId,
                    EMail = s.EMail,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    PhoneNumber = s.PhoneNumber
                }).ToListAsync(cancellationToken);
        }

        public async Task<Organization> AddUserToOrganizationAsync(int userId, int organizationId, CancellationToken cancellationToken)
        {
            await _repository.AddUserToOrganizationAsync(userId, organizationId, cancellationToken);
            var orgDto = await _repository.GetOrganizations()
                .Include(org => org.Users)
                .FirstOrDefaultAsync(f => f.OrganizationId == organizationId);

            if (orgDto == null) throw new EntityNotFoundException(nameof(Organization), organizationId);

            return new Organization
            {
                Name = orgDto.Name,
                OrganizationId = orgDto.OrganizationId,
                Users = orgDto.Users.Select(s => new User
                {
                    Id = s.UserId,
                    EMail = s.EMail,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    PhoneNumber = s.PhoneNumber
                }).ToList()
            };
        }

        public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _repository
                .GetUsers()
                .Select(u => new User
                {
                    EMail = u.EMail,
                    FirstName = u.FirstName,
                    Id = u.UserId,
                    LastName = u.LastName,
                    MiddleName = u.MiddleName,
                    PhoneNumber = u.PhoneNumber
                }).ToListAsync(cancellationToken);
        }

        public async Task<List<Organization>> GetOrganizationsAsync(CancellationToken cancellationToken)
        {
            return await _repository
                .GetOrganizations()
                .Include(org => org.Users)
                .Select(org => new Organization
                {
                    Name = org.Name,
                    OrganizationId = org.OrganizationId,
                    Users = org.Users.Select(u => new User
                    {
                        EMail = u.EMail,
                        FirstName = u.FirstName,
                        Id = u.UserId,
                        LastName = u.LastName,
                        MiddleName = u.MiddleName,
                        PhoneNumber = u.PhoneNumber
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }

        public async Task<Organization> GetOrganizationAsync(int organizationId, CancellationToken cancellationToken)
        {
            var orgDto = await _repository.GetOrganizations()
                .Include(org => org.Users)
                .FirstOrDefaultAsync(f => f.OrganizationId == organizationId, cancellationToken);

            if (orgDto == null) throw new EntityNotFoundException(nameof(Organization), organizationId);

            return new Organization
            {
                Name = orgDto.Name,
                OrganizationId = orgDto.OrganizationId,
                Users = orgDto.Users.Select(s => new User
                {
                    Id = s.UserId,
                    EMail = s.EMail,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    PhoneNumber = s.PhoneNumber
                }).ToList()
            };
        }
    }
}

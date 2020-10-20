using AutoMapper;
using CommonData;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Receiver.Exceptions;
using Receiver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver
{
    public class ReceiverService : IReceiverService
    {
        private const int PAGE_SIZE_DEFAULT = 3;
        private const int PAGE_NUMBER_DEFAULT = 1;

        private readonly IReceiverRepository _repository;
        private readonly IMapper _mapper;

        // Признак выполнена ли первичная инициализация репозитория
        private static bool isInitRepository = false;

        public ReceiverService(IReceiverRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            InitializeReceiverRepository();
        }

        public async Task<int> AddUserAsync(IUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userDTO = _mapper.Map<IUser, UserDTO>(user);
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
            var countEntities = await _repository.GetUsers()
                .Where(w => w.Organization.OrganizationId == organizationId)
                .CountAsync(cancellationToken);

            var result = countEntities / ((pageSize < 1) ? PAGE_SIZE_DEFAULT : pageSize);

            return result + 1;
        }

        public async Task<List<User>> GetUsersPaginationAsync(int organizationId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var validPageSize = (pageSize < 1) ? PAGE_SIZE_DEFAULT : pageSize;
            var validPageNumber = (pageNumber < 1) ? PAGE_NUMBER_DEFAULT : pageNumber;

            var pageCount = await GetUsersPageCountAsync(organizationId, pageSize, cancellationToken);

            if (validPageNumber > pageCount)
                validPageNumber = pageCount;

            var users = await _repository.GetUsers()
                .Where(w => w.Organization.OrganizationId == organizationId)
                .Skip((validPageNumber - 1) * validPageSize)
                .Take(validPageSize)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<UserDTO>, List<User>>(users);
        }

        public async Task<Organization> AddUserToOrganizationAsync(int userId, int organizationId, CancellationToken cancellationToken)
        {
            await _repository.AddUserToOrganizationAsync(userId, organizationId, cancellationToken);
            var orgDto = await _repository.GetOrganizations()
                .Include(org => org.Users)
                .FirstOrDefaultAsync(f => f.OrganizationId == organizationId);

            if (orgDto == null) throw new EntityNotFoundException(nameof(Organization), organizationId);

            return _mapper.Map<OrganizationDTO, Organization>(orgDto);
        }

        public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var userList = await _repository
                .GetUsers()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<UserDTO>, List<User>>(userList);
        }

        public async Task<List<Organization>> GetOrganizationsAsync(CancellationToken cancellationToken)
        {
            var orgList = await _repository
                .GetOrganizations()
                .Include(org => org.Users)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<OrganizationDTO>, List<Organization>>(orgList);
        }

        public async Task<Organization> GetOrganizationAsync(int organizationId, CancellationToken cancellationToken)
        {
            var orgDto = await _repository.GetOrganizations()
                .Include(org => org.Users)
                .FirstOrDefaultAsync(f => f.OrganizationId == organizationId, cancellationToken);

            if (orgDto == null) throw new EntityNotFoundException(nameof(Organization), organizationId);

            return _mapper.Map<OrganizationDTO, Organization>(orgDto);
        }
    }
}

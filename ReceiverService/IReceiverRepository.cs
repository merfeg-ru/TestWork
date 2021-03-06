﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Receiver.Models;

namespace Receiver
{
    public interface IReceiverRepository
    {
        /// <summary>
        /// Первоначальная инициализация данных (seed)
        /// </summary>
        void Initialize();

        Task<int> AddUserAsync(UserDTO user, CancellationToken cancellationToken);

        IQueryable<UserDTO> GetUsers();

        IQueryable<OrganizationDTO> GetOrganizations();

        Task AddUserToOrganizationAsync(int userId, int organizationId, CancellationToken cancellationToken);

        Task<UserDTO> UpdateUserAsync(UserDTO user, CancellationToken cancellationToken);
    }
}

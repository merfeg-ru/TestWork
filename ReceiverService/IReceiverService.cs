using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonData;
using MassTransit;
using Receiver.Models;

namespace Receiver
{
    public interface IReceiverService
    {
        void InitializeReceiverRepository();

        Task<int> AddUserAsync(IUser user, CancellationToken cancellationToken);

        Task<int> GetUsersPageCountAsync(int organizationId, int pageSize, CancellationToken cancellationToken);

        Task<List<User>> GetUsersPaginationAsync(int organizationId, int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<List<User>> GetUsersAsync(CancellationToken cancellationToken);

        Task<List<Organization>> GetOrganizationsAsync(CancellationToken cancellationToken);

        Task<Organization> GetOrganizationAsync(int organizationId, CancellationToken cancellationToken);

        Task<Organization> AddUserToOrganizationAsync(int userId, int organizationId, CancellationToken cancellationToken);
    }
}

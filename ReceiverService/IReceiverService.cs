using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonData;
using MassTransit;
using ReceiverService.Models;

namespace ReceiverService
{
    public interface IReceiverService
    {
        void InitializeReceiverRepository();

        Task<int> AddUserAsync(IUser user);

        Task<List<User>> GetUsersPaginationAsync(int organizationId, int pageNumber, int pageSize);

        Task AddUserToOrganizationAsync(int userId, int organizationId);
    }
}

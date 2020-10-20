using MediatR;
using Receiver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receiver.Queries
{
    public class GetListUsersOrganizationQuery : IRequest<List<User>>
    {
        public int OrganizationId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public GetListUsersOrganizationQuery(int organizationId, int pageSize, int pageNumber)
        {
            OrganizationId = organizationId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}

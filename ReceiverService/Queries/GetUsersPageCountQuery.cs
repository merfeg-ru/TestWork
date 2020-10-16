using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceiverService.Queries
{
    public class GetUsersPageCountQuery : IRequest<int>
    {
        public int OrganizationId { get; set; }
        public int PageSize { get; set; }

        public GetUsersPageCountQuery(int organizationId, int pageSize)
        {
            OrganizationId = organizationId;
            PageSize = pageSize;
        }
    }
}

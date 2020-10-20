using MediatR;
using Receiver.Models;

namespace Receiver.Queries
{
    public class GetOrganizationQuery : IRequest<Organization>
    {
        public int OrganizationId { get; set; }

        public GetOrganizationQuery(int organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}

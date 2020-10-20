using MediatR;
using Receiver.Models;

namespace Receiver.Commands
{
    public class AddUserOrganizationCommand : IRequest<Organization>
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }

        public AddUserOrganizationCommand(int organizationId, int userId)
        {
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}

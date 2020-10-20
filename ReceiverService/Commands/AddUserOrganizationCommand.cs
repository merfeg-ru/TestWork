using MediatR;
using Receiver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

using MediatR;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiverService.Commands
{
    public class AddUserOrganizationCommandHandler : IRequestHandler<AddUserOrganizationCommand, Organization>
    {
        private readonly IReceiverService _receiverService;
        public AddUserOrganizationCommandHandler(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        public async Task<Organization> Handle(AddUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            return await _receiverService.AddUserToOrganizationAsync(request.UserId, request.OrganizationId, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

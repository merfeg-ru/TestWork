using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Receiver.Models;


namespace Receiver.Commands
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

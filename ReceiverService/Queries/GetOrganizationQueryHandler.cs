using MediatR;
using Receiver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver.Queries
{
    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, Organization>
    {
        private readonly IReceiverService _receiverService;

        public GetOrganizationQueryHandler(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            return await _receiverService.GetOrganizationAsync(request.OrganizationId, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

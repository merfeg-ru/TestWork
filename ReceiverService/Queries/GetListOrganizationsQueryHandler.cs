using MediatR;
using ReceiverService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiverService.Queries
{
    public class GetListOrganizationsQueryHandler : IRequestHandler<GetListOrganizationsQuery, List<Organization>>
    {
        private readonly IReceiverService _receiverService;

        public GetListOrganizationsQueryHandler(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        public async Task<List<Organization>> Handle(GetListOrganizationsQuery request, CancellationToken cancellationToken)
        {
            return await _receiverService.GetOrganizationsAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Receiver.Models;

namespace Receiver.Queries
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

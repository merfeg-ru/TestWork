using MediatR;
using Receiver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver.Queries
{
    public class GetListUsersOrganizationQueryHandler : IRequestHandler<GetListUsersOrganizationQuery, List<User>>
    {
        private readonly IReceiverService _receiverService;

        public GetListUsersOrganizationQueryHandler(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        public async Task<List<User>> Handle(GetListUsersOrganizationQuery request, CancellationToken cancellationToken)
        {
            return await _receiverService.GetUsersPaginationAsync(request.OrganizationId, request.PageNumber, request.PageSize, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Receiver.Models;

namespace Receiver.Queries
{
    public class GetListUsersQueryHandler : IRequestHandler<GetListUsersQuery, List<User>>
    {
        private readonly IReceiverService _receiverService;

        public GetListUsersQueryHandler(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        public async Task<List<User>> Handle(GetListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _receiverService.GetUsersAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

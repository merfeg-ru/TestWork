﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiverService.Queries
{
    public class GetUsersPageCountQueryHandler : IRequestHandler<GetUsersPageCountQuery, int>
    {
        private readonly IReceiverService _receiverService;

        public GetUsersPageCountQueryHandler(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        public async Task<int> Handle(GetUsersPageCountQuery request, CancellationToken cancellationToken)
        {
            return await _receiverService.GetUsersPageCountAsync(request.OrganizationId, request.PageSize, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

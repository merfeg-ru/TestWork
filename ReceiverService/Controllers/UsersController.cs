using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;

using Receiver.Models;
using Receiver.Queries;

namespace Receiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get(CancellationToken cancellationToken)
        {
            var query = new GetListUsersQuery();
            return await _mediator.Send(query, cancellationToken);
        }
    }
}

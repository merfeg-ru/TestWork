using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReceiverService.Models;
using ReceiverService.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiverService.Controllers
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

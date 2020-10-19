using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Receiver.Models;
using Receiver.Queries;
using Receiver.Commands;

namespace Receiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IEnumerable<Organization>> Get(CancellationToken cancellationToken)
        {
            var query = new GetListOrganizationsQuery();
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Organization> GetOrganization([FromRoute]int id, CancellationToken cancellationToken)
        {
            var query = new GetOrganizationQuery(id);
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        [Route("{id}/pagecount")]
        public async Task<int> GetPagesCount([FromRoute]int id, CancellationToken cancellationToken, [FromQuery]int pageSize = 3)
        {
            var query = new GetUsersPageCountQuery(id, pageSize);
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPost]
        [Route("{id}/page")]
        public async Task<IEnumerable<User>> GetUsers([FromRoute]int id, CancellationToken cancellationToken, [FromQuery]int pageSize = 3, [FromQuery]int pageNumber = 1)
        {
            var query = new GetListUsersOrganizationQuery(id, pageSize, pageNumber);
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPost]
        [Route("{id}/adduser")]
        public async Task<Organization> AddUser([FromRoute]int id, [FromQuery]int userId, CancellationToken cancellationToken)
        {
            var command = new AddUserOrganizationCommand(id, userId);
            return await _mediator.Send(command, cancellationToken);
        }
    }
}

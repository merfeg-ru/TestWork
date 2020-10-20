using System;
using System.Threading;
using System.Threading.Tasks;
using Sender.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sender.Commands;

namespace Sender.Controllers
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

        [HttpPost]
        public async Task<IActionResult> Post(User user, CancellationToken cancellationToken)
        {
            bool commandResult = await _mediator.Send(new SenderCommand(user), cancellationToken);

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}

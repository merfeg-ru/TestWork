using System;
using System.Threading;
using System.Threading.Tasks;
using SenderService.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SenderService.Controllers
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
            bool commandResult = await _mediator.Send(user);

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

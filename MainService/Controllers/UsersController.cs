using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MainService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IDataSenderService _dataSenderService;

        public UsersController(ILogger<UsersController> logger, IDataSenderService dataSenderService)
        {
            _logger = logger;
            _dataSenderService = dataSenderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user, CancellationToken cancellationToken)
        {
            await _dataSenderService.Send(user, cancellationToken);
            return Ok(user);
        }

        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}

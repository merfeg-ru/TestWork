using System;
using System.Collections.Generic;
using System.Linq;
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

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            if (ModelState.IsValid)
                return Ok(user);
            else
                return ValidationProblem();
        }

        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}

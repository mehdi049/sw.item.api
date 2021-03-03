using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SW.Item.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class HealthCheckController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}

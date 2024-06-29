using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiPeople.Middlewares;

namespace ApiPeople.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : Controller
    {
        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        // GET: api/sample/unprotected-route
        [HttpGet("unprotected-route")]
        public IActionResult UnprotectedRoute()
        {
            _logger.LogInformation("Accessed unprotected route");
            return Ok("This is an unprotected route");
        }

        // GET: api/sample/protected-route
        [HttpGet("protected-route-admin")]
        [TypeFilter(typeof(JwtAuthorizeAttribute), Arguments = new object[] { "Admin" })]
        public IActionResult ProtectedRoute()
        {
            _logger.LogInformation("Accessed protected route, only for admins");
            return Ok("This is a protected route");
        }    

        // GET: api/sample/protected-route
        [HttpGet("protected-route-customer")]
        [TypeFilter(typeof(JwtAuthorizeAttribute), Arguments = new object[] { "Customer" })]
        public IActionResult ProtectedRouteCustomer()
        {
            _logger.LogInformation("Accessed protected route, only for customes");
            return Ok("This is a protected route");
        }
    }
}

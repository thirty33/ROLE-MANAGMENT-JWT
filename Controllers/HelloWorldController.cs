using ApiPeople.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeople.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : Controller
    {

        IHelloWorldService helloWorldService;

        public HelloWorldController(IHelloWorldService helloWorld)
        {
            helloWorldService = helloWorld;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(helloWorldService.GetHelloWorld());
        }
    }
}

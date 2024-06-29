using ApiPeople.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeople.Controllers
{

    [Route("api/[controller]")]
    public class TareaController : ControllerBase
    {
        ITaskService tareasService;

        public TareaController(ITaskService service)
        {
            tareasService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(tareasService.Get());
        }

    }
}

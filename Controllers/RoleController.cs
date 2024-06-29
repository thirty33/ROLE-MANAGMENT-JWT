using ApiPeople.Models;
using ApiPeople.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApiPeople.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            var roles = await _roleService.GetRoles();
            return Ok(roles);
        }
    }
}

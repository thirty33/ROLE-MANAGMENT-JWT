using ApiPeople.Middlewares;
using ApiPeople.Models;
using ApiPeople.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using System.Data;

namespace ApiPeople.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService UserService;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public UserController(
            IUserService service,
            IJwtAuthenticationService jwtAuthenticationService,
            ILogger<UserController> logger
        )
        {
            UserService = service;
            _jwtAuthenticationService = jwtAuthenticationService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(UserService.Get());
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();

            }

            // Crear un nuevo objeto User a partir del DTO
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email
            };

            try
            {
                // Llamar al servicio para guardar el usuario con el rol
                await UserService.Save(user, userDto.RoleName);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null");
            }

            try
            {
                // Llamar al servicio para actualizar el usuario
                await UserService.Update(id, userDto);
                return Ok("User updated successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Login data is null");
            }

            var user = await UserService.Authenticate(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtAuthenticationService.Authenticate(user.Username, user);

            return Ok(new { Token = token });

        }

    }
}

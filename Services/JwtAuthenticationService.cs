using ApiPeople.Context;
using ApiPeople.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiPeople.Services
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly IConfiguration _configuration;
        AppDbContext context;

        public JwtAuthenticationService(IConfiguration configuration, AppDbContext dbcontext)
        {
            _configuration = configuration;
            context = dbcontext;
        }

        public string Authenticate(string username, User user)
        {
            var loggedUser = context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefault(u => u.Id == user.Id);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSecret"]); // Obtiene la clave secreta desde la configuración

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, string.Join(",", loggedUser.UserRoles.Select(ur => ur.Role.Name)))
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Expiración del token en una hora
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }

    public interface IJwtAuthenticationService
    {
        string Authenticate(string username, User user);
    }
}

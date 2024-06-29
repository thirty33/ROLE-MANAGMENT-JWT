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
        private readonly ILogger<JwtAuthenticationService> _logger;

        public JwtAuthenticationService(IConfiguration configuration, AppDbContext dbcontext, ILogger<JwtAuthenticationService> logger)
        {
            _configuration = configuration;
            context = dbcontext;
            _logger = logger;
        }

        public string Authenticate(string username, User user)
        {
            var loggedUser = context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Id == user.Id);

            if (loggedUser == null)
            {
                throw new ArgumentException("User not found");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSecret"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            // Adding roles to claims
            claims.AddRange(loggedUser.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name)));

            // Logging claims
            _logger.LogDebug("Claims being added to token:");
            foreach (var claim in claims)
            {
                _logger.LogDebug($"Type: {claim.Type}, Value: {claim.Value}");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
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

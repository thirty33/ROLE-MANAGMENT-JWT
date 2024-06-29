using ApiPeople.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ApiPeople.Middlewares
{
    public class JwtAuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        private readonly ILogger<JwtAuthorizeAttribute> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _role;

        public JwtAuthorizeAttribute(
            IConfiguration configuration,
            ILogger<JwtAuthorizeAttribute> logger,
            string role
        )
        {
            _configuration = configuration;
            _logger = logger;
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSecret"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Log all claims
                _logger.LogDebug("Claims in token:");
                foreach (var claim in jwtToken.Claims)
                {
                    _logger.LogDebug($"Type: {claim.Type}, Value: {claim.Value}");
                }

                var usernameClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name) ?? jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name");

                if (usernameClaim == null)
                {
                    _logger.LogError("Username claim not found in token.");
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var roleClaims = jwtToken.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

                // Log role claims
                if (roleClaims.Count == 0)
                {
                    _logger.LogWarning("No role claims found in token.");
                }
                else
                {
                    _logger.LogInformation($"Role Claims: {string.Join(", ", roleClaims)}");
                }

                if (!roleClaims.Contains(_role))
                {
                    _logger.LogInformation($"User does not have the required role: {_role}");
                    context.Result = new ForbidResult();
                    return;
                }

                var username = usernameClaim.Value;
                _logger.LogDebug("Username: " + username);

                // Attach user to context on successful jwt validation
                context.HttpContext.Items["User"] = username;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token validation failed.");
                context.Result = new UnauthorizedResult();
            }
        }
    }
}

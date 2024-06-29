using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
namespace ApiPeople.Middlewares
{
    //public class JwtAuthorizeFilter : IAuthorizationFilter
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly ILogger<JwtAuthorizeFilter> _logger;
    //    private readonly string _role;

    //    public JwtAuthorizeFilter(string role, ILogger<JwtAuthorizeFilter> logger)
    //    {
    //        _configuration = _configuration ?? throw new ArgumentNullException(nameof(configuration));
    //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //        _role = role ?? throw new ArgumentNullException(nameof(role));
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var attribute = new JwtAuthorizeAttribute(_configuration, _logger, _role);
    //        attribute.OnAuthorization(context);
    //    }
    //}
}

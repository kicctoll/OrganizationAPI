using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Infrastructure.Services;
using Infrastructure.Utilities;
using Infrastructure.Utilities.FacebookAuthentication;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly FacebookAuthenticationService _facebookAuthenticationService = null;
        private readonly IConfiguration _configuration = null;

        public AccountController(FacebookAuthenticationService facebookAuthenticationService, IConfiguration configuration)
        {
            _facebookAuthenticationService = facebookAuthenticationService;
            _configuration = configuration;
        }

        [HttpGet("facebook-login")]
        public IActionResult Login()
        {
            string appId = _configuration["Authentication:Facebook:AppId"];
            string redirectURI = "https://" + Request.Host.Value + Url.Action(nameof(LoginCallback));

            return Redirect($"{FacebookDefaultOptions.LoginURI}" +
                $"client_id={appId}&" +
                $"redirect_uri={redirectURI}&" +
                $"response_type=code&" +
                $"scope=email,user_location");
        }

        [HttpGet("facebook-callback")]
        public async Task<IActionResult> LoginCallback(string code, string error)
        {
            if (error == null)
            {
                // Getting user access token
                string redirect_uri = "https://" + Request.Host.Value + Url.Action(nameof(LoginCallback));
                var response = await _facebookAuthenticationService.Login(code, redirect_uri);

                return Ok(response);
            }

            string errorReason = Request.Query["error_reason"];
            string errorDescription = Request.Query["error_description"];
            return Unauthorized(new { Error = error, ErrorReason = errorReason, ErrorDescription = errorDescription });
        }
    }
}
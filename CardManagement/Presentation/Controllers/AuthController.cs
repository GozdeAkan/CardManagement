using AutoMapper;
using Business.Contracts;
using Business.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides authentication and authorization endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identitiyService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="identitiyService">Service to handle authentication and authorization logic.</param>
        public AuthController(IIdentityService identitiyService)
        {
            _identitiyService = identitiyService;
        }
        /// <summary>
        /// Logs a user in and returns a token if the credentials are valid.
        /// </summary>
        /// <param name="model">The login credentials (username and password).</param>
        /// <returns>A <see cref="TokenDto"/> containing the access token and its details.</returns>

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto model)
        {
            var tokenModel = await _identitiyService.Login(model);
         
            return tokenModel;
        }

        /// <summary>
        /// Registers a new user and returns a token upon successful registration.
        /// </summary>
        /// <param name="model">The registration details (e.g., username, password, email).</param>
        /// <returns>A <see cref="TokenDto"/> containing the access token and its details.</returns>
        [HttpPost("register")]
        public async Task<ActionResult<TokenDto>> Register(RegisterDto model)
        {
            return await _identitiyService.Register(model);
        }


    }
}

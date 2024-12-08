
using Business.DTOs.Auth;

namespace Business.Contracts
{
    /// <summary>
    /// Defines methods for user authentication and registration.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Authenticates a user and generates an access token if the credentials are valid.
        /// </summary>
        /// <param name="model">The login credentials, such as username and password.</param>
        /// <returns>A <see cref="TokenDto"/> containing the generated access token and its details, or null if authentication fails.</returns>

        Task<TokenDto?> Login(LoginDto model);

        /// <summary>
        /// Registers a new user and generates an access token upon successful registration.
        /// </summary>
        /// <param name="model">The registration details, including username, password, and email.</param>
        /// <returns>A <see cref="TokenDto"/> containing the generated access token and its details, or null if registration fails.</returns>

        Task<TokenDto?> Register(RegisterDto model);
    }
}

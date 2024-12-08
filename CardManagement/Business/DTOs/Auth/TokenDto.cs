using System;

namespace Business.DTOs.Auth
{
    /// <summary>
    /// Represents a data transfer object for authentication tokens.
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Gets or sets the access token used for authentication and authorization.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token used to renew the access token when it expires.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}

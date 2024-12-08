namespace Business.DTOs.Auth
{
    /// <summary>
    /// Represents the data transfer object for user registration.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the email address of the user to register.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the user to register.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}

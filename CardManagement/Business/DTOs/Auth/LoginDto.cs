namespace Business.DTOs.Auth
{
    /// <summary>
    /// Represents the data transfer object for user login.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the email address of the user attempting to log in.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the user attempting to log in.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}

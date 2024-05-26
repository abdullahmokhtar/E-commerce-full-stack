namespace Backend.API.Dtos.Account
{
    public class SignUpDto
    {
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password length between 8 to 16 characters")]
        [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$", ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 special character, 1 digit and length 8 characters")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password")]
        public string RePassword { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}

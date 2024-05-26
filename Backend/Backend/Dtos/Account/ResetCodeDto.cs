namespace Backend.API.Dtos.Account
{
    public class ResetCodeDto
    {
        [Required]
        [Length(6, 6)]
        public string ResetCode { get; set; } = string.Empty;
    }
}

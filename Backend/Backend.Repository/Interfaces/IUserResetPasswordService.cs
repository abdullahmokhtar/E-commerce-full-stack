namespace Backend.BLL.Interfaces
{
    public interface IUserResetPasswordService
    {
        Task<string> GenerateCode(string email);
        Task<bool> VerifyCode(string code);
        Task<bool> IsVerified(string email);
        Task DeleteCode(string userId);
    }
}

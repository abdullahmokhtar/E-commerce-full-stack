namespace Backend.BLL.Interfaces
{
    public interface IUserResetCodeRepository
    {
        Task<UserResetCode?> GetAsync(string userId);
        Task<UserResetCode?> GetByCodeAsync(string code);
        Task<UserResetCode?> AddAsync(UserResetCode model);
        Task<bool> DeleteAsync(UserResetCode model);
        Task<bool> UpdateAsync(UserResetCode model);
    }
}

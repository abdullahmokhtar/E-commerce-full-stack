using Microsoft.AspNetCore.Identity;

namespace Backend.BLL.Services
{
    public class UserResetPasswordService : IUserResetPasswordService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserResetCodeRepository _userResetCodeRepository;

        public UserResetPasswordService(UserManager<AppUser> userManager, IUserResetCodeRepository userResetCodeRepository )
        {
            _userManager = userManager;
            _userResetCodeRepository = userResetCodeRepository;
        }

        public async Task DeleteCode(string userId)
        {
            var userCode = await _userResetCodeRepository.GetAsync(userId);

            if (userCode != null)
                await _userResetCodeRepository.DeleteAsync(userCode);
        }

        public async Task<string> GenerateCode(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            await DeleteCode(user.Id);

            Random r = new Random();
            var x = r.Next(0, 1000000);
            string s = x.ToString("000000");

            var newUserResetCode = new UserResetCode 
            { 
                AppUserId = user.Id,
                Code = s
            };

            var userWithNewCode = await _userResetCodeRepository.AddAsync(newUserResetCode);

            if (userWithNewCode == null)
                return null;

            return s;

        }

        public async Task<bool> IsVerified(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;

            var userCode = await _userResetCodeRepository.GetAsync(user.Id);

            if (userCode is null)
                return false;

            return userCode.IsCodeVerified;
        }

        public async Task<bool> VerifyCode(string code)
        {
            var userResetCode = await _userResetCodeRepository.GetByCodeAsync(code);

            if (userResetCode == null)
                return false;

            if(userResetCode.Code == code && userResetCode.CreatedAt.AddMinutes(15) >= DateTime.Now)
            {
                userResetCode.IsCodeVerified = true;

                return await _userResetCodeRepository.UpdateAsync(userResetCode);
            }
            return false;

        }
    }
}

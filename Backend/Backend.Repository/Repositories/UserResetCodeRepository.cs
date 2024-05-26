namespace Backend.BLL.Repositories
{
    public class UserResetCodeRepository : IUserResetCodeRepository
    {
        private readonly AppDbContext _context;

        public UserResetCodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResetCode?> AddAsync(UserResetCode model)
        {
            await _context.UserResetCodes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(UserResetCode model)
        {
            _context.UserResetCodes.Remove(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserResetCode?> GetAsync(string userId)
            => await _context.UserResetCodes.FirstOrDefaultAsync(e => e.AppUserId == userId);

        public async Task<UserResetCode?> GetByCodeAsync(string code)
         => await _context.UserResetCodes.FirstOrDefaultAsync(e => e.Code ==  code);

        public async Task<bool> UpdateAsync(UserResetCode model)
        {
            _context.UserResetCodes.Update(model);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}

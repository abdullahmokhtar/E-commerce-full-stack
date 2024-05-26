namespace Backend.BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);

    }
}

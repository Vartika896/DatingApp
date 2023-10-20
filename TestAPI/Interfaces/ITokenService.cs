using TestAPI.Entities;

namespace TestAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
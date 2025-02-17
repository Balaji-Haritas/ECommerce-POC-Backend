using EcommercePOC.Models;

namespace EcommercePOC.RepositoryInterface
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}

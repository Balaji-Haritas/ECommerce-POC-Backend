using EcommercePOC.Models;

namespace EcommercePOC.RepositoryInterface
{
    public interface IRoleRepository
    {
        Task<Role> AddRoleAsync(string roleName);
        Task<Role> GetRoleByIdAsync(int id);
        Task<IEnumerable<Role>> GetRolesAsync();
    }
}

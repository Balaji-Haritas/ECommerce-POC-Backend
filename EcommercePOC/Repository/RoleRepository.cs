using EcommercePOC.DataAccess;
using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace EcommercePOC.Repository
{
    public class RoleRepository:IRoleRepository
    {
        public readonly AppDbContext _dbContext;


        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> AddRoleAsync(string roleName)
        {
            var role = new Role { RoleName = roleName };
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }
    }
}

using ApiPeople.Context;
using ApiPeople.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPeople.Services
{
    public class RoleService: IRoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }

    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
    }
}

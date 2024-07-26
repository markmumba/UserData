using UserData.Data;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserData.Models;

namespace UserData.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserContext _context;

        public UserRoleRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetUserRoles()
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<UserRole> GetUserRolesById(int id)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.Id == id);
        }

        public async Task<UserRole> AddUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return userRole;
        }

        public async Task UpdateUserRole(UserRole userRole)
        {
            _context.Entry(userRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserRole(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
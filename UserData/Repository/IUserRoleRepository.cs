using UserData.Models;

namespace UserData.Repository
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetUserRoles();
        Task<UserRole> GetUserRolesById(int id);
        Task<UserRole> AddUserRole(UserRole userRole);
        Task UpdateUserRole(UserRole userRole);
        Task DeleteUserRole(int id);
    }
}

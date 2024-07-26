using UserData.Models;

namespace UserData.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> AddRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int id);
    }
}

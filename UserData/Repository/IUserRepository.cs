using UserData.Models;

namespace UserData.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}

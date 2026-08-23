using Task6.Models;

namespace Task6.Services.Interfaces
{
    public interface IUsersService
    {

        Task<User> CreateUser(User newUser);
        Task<User?> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}

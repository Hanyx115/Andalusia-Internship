using Task6.Models;

namespace Task6.Repistories.Interfaces;

public interface IUsersRepository
{
    Task<User> CreateUser(User newUser);
    Task<User?> GetUserById(int id);
    Task<IEnumerable<User>> GetAllUsers();
    Task UpdateUser(User user);
    Task DeleteUser(int id);
    Task<User?> GetUserByEmail(string email);
}
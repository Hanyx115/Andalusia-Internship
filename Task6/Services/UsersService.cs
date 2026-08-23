using Task6.Models;
using Task6.Repistories.Interfaces;
using Task6.Services.Interfaces;


namespace Task6.Api.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _userRepo;

    public UsersService(IUsersRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<User> CreateUser(User newUser)
    {
        return await _userRepo.CreateUser(newUser);
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userRepo.GetUserById(id);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepo.GetAllUsers();
    }

    public async Task UpdateUser(User user)
    {
        await _userRepo.UpdateUser(user);
    }

    public async Task DeleteUser(int id)
    {
        await _userRepo.DeleteUser(id);
    }
}
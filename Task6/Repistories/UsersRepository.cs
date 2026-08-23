using Microsoft.EntityFrameworkCore;
using Task6.Data;
using Task6.Models;
using Task6.Repistories.Interfaces;

namespace Task6.Repistories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public UsersRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<User> CreateUser(User newUser)
        {
            _dbcontext.Users.Add(newUser);
            await _dbcontext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _dbcontext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dbcontext.Users.ToListAsync();
        }

        public async Task UpdateUser(User user)
        {
            _dbcontext.Users.Update(user);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _dbcontext.Users.FindAsync(id);
            if (user != null)
            {
                _dbcontext.Users.Remove(user);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

using BurgerProject.Data;
using BurgerProject.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BurgerProject.Repositories
{
    public class UserRepo : IUserRepo
    {
        public readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async  Task DeleteUserAsync(string number)
        {
            var user = await _context.Users.FindAsync(number);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

        }

            public  async Task<IEnumerable<User>> GetAllUsersAsync()
            {
                return await _context.Users.ToListAsync();
               
            }

        public async Task<User> GetUserByIdAsync(string number)
        {
            return await _context.Users.FindAsync(number);
        }

        public async Task SaveUserAsync()
        {
            await _context.SaveChangesAsync();
        }

        public  async Task UpdateUserAsync(User user)
        {
            var ExistingUser = _context.Users.Local.FirstOrDefault(c => c.Number == user.Number);

            if (ExistingUser != null)
            {
                _context.Entry(ExistingUser).State = EntityState.Detached;
            }

            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

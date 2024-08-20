using BurgerProject.Model;

namespace BurgerProject.Repositories
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string number);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string number);
        Task SaveUserAsync();
    }
}

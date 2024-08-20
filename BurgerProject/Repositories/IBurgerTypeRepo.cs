using BurgerProject.Model;

namespace BurgerProject.Repositories
{
    public interface IBurgerTypeRepo
    {
        Task<IEnumerable<BurgerType>> GetAllBurgerTypeAsync();
        Task<BurgerType> GetBurgerTypeByIdAsync(int id);
        Task SaveBurgerTypeAsync();
    }
}

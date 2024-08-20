using BurgerProject.Model;

namespace BurgerProject.Repositories
{
    public interface IBurgerRepo
    {
        Task<IEnumerable<Burger>> GetAllBurgerAsync();
        Task<Burger> GetBurgerByIdAsync(int id);  
        Task SaveBurgerAsync();
    }
}

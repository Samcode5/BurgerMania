using BurgerProject.Model;

namespace BurgerProject.Repositories
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task SaveOrderAsync();
    }
}

using BurgerProject.Data;
using BurgerProject.Model;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BurgerProject.Repositories
{
    public class OrderRepo : IOrderRepo
    {

        private readonly AppDbContext _context;

        public OrderRepo(AppDbContext context)
        {
            _context = context;
            
        }
        public  async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _context.Orders.ToListAsync();

        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task SaveOrderAsync()
        {
           await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var Existingorder = _context.Orders.Local.FirstOrDefault(c => c.Id == order.Id);

            if (Existingorder != null)
            {
                _context.Entry(Existingorder).State = EntityState.Detached;
            }

            _context.Orders.Attach(order);
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

      
    }
}

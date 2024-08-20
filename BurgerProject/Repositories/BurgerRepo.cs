using BurgerProject.Data;
using BurgerProject.Model;
using Microsoft.EntityFrameworkCore;

namespace BurgerProject.Repositories
{
    public class BurgerRepo : IBurgerRepo
    {
        public readonly AppDbContext _context;

        public BurgerRepo(AppDbContext context)
        {
            _context = context;
            
        }
        public  async Task<IEnumerable<Burger>> GetAllBurgerAsync()
        {
            return await _context.Burgers.ToListAsync();
        }

        public  async Task<Burger> GetBurgerByIdAsync(int id)
        {
            return await _context.Burgers.FindAsync(id);
        }

        public async Task SaveBurgerAsync()
        {
           await  _context.SaveChangesAsync();
        }
    }
}

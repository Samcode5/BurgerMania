using BurgerProject.Data;
using BurgerProject.Model;
using Microsoft.EntityFrameworkCore;

namespace BurgerProject.Repositories
{
    public class BurgerTypeRepo : IBurgerTypeRepo
    {
        private readonly AppDbContext _context;

        public BurgerTypeRepo(AppDbContext context)
        {
            _context = context;
            
        }
        public  async Task<IEnumerable<BurgerType>> GetAllBurgerTypeAsync()
        {
             return await _context.BurgerTypes.ToListAsync();
        }

        public  async Task<BurgerType> GetBurgerTypeByIdAsync(int id)
        {
            return await _context.BurgerTypes.FindAsync(id);  
        }

        public async  Task SaveBurgerTypeAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using BurgerProject.Model;

namespace BurgerProject.Data
{
    public class AppDbContext:DbContext
    {
       public  DbSet<Burger> Burgers { get; set; }

       public DbSet<BurgerType> BurgerTypes { get; set; }

       public DbSet<User> Users { get; set; }  

       public DbSet<Order> Orders { get; set; }

     

         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(oi => oi.Burger) // Order has one Burger
                .WithMany() // burgers can have many orderitems
                .HasForeignKey(oi => oi.BurgerId)
                 .OnDelete(DeleteBehavior.Cascade);
            
            

            modelBuilder.Entity<Order>()
                .HasOne(oi => oi.BurgerType) // Order has one BurgerType
                .WithMany()   // burgertype can have many orderitems
                .HasForeignKey(oi => oi.BurgeTypeId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Order>()
           .HasOne(oi => oi.User) // Order has one user
           .WithMany()   // user can have many orders
           .HasForeignKey(oi => oi.UserNumber)
           .OnDelete(DeleteBehavior.Cascade);



        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerProject.Data;
using BurgerProject.Controllers;
using BurgerProject.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class TestDatabaseFixture : IAsyncLifetime
{
    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=BurgerManiaTestDb;Trusted_Connection=True;ConnectRetryCount=0";

    public AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        return new AppDbContext(options);
    }

    public async Task InitializeAsync()
    {
        using (var context = CreateContext())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            context.Users.AddRange(
                new User
                {
                    Number="9970100283",
                    Name="Sanjay",
                    Password="Sanjay123",
                    Role="User"
                },

                 new User
                 {
                     Number = "9402855075",
                     Name = "Manisha",
                     Password = "Manisha3212",
                     Role="Admin"
                 }


            );
            await context.SaveChangesAsync();
        }
    }

    public async Task DisposeAsync()
    {
        using (var context = CreateContext())
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
}
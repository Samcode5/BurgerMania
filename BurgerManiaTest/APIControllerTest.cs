using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using BurgerProject.Controllers;
using BurgerProject.Model;
using BurgerProject.Repositories;
namespace BurgerManiaTest
{
    [Collection("TransactionalTests")]
    public class APIControllerTest:IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _fixture;

        public APIControllerTest(TestDatabaseFixture fixture)
        {
            _fixture = fixture;
        }



        [Fact]
        public async Task GetUsers_ReturnsListOfUser()
        {
            // Arrange
            var controller = new UserRepo(_fixture.CreateContext());

            // Act
            var result = await controller.GetAllUsersAsync();

            // Assert
            var returnValue = Assert.IsType<List<User>>(result); // Change to List<User> 
            Console.WriteLine($"{returnValue}" + "returnValue");
            Assert.Equal(2, returnValue.Count);

        }

    }
}

using System.ComponentModel;

namespace BurgerProject.DTOs
{
    public class UserDTO
    {
        public string Number { get; set; }


        public string Name { get; set; }

        public string Password { get; set; }

        [DefaultValue("User")]
        public string Role { get; set; }
    }
}

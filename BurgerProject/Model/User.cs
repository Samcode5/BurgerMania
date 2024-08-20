using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerProject.Model
{
    [Table("Users")]
    public class User
    {

        [Key] 
        public string Number { get; set; }


        public string Name { get; set; }

        public string Password { get; set; }


        [DefaultValue("User")]
        public string Role { get; set; }


    }
}

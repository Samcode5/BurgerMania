using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerProject.Model
{
    [Table("Burgers")]
    public class Burger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BurgerId { get; set; }

        public string BurgerName { get; set; }

    }
}

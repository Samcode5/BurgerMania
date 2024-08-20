using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BurgerProject.Model
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }


        public string UserNumber  { get; set; }

        public User User { get; set; }

        public int BurgerId { get; set; }

        public Burger Burger { get; set; }


        public int BurgeTypeId  { get; set; }


        public BurgerType BurgerType { get; set; }




        public int quantity { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerProject.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }


        public string  UserNumber { get; set; }

       

        public int BurgerId { get; set; }

    

        public int BurgeTypeId { get; set; }




        public int quantity { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}

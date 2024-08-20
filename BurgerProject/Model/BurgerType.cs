using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerProject.Model
{
    [Table("BurgerTypes")]
    public class BurgerType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Type  { get; set; }
    }
}

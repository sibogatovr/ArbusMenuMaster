using System.ComponentModel.DataAnnotations;

namespace ArbusMenuMaster
{
    public class Dish
    {
        [Required]
        public string Name { get; set; }
    }
}

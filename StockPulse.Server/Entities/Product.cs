using System.ComponentModel.DataAnnotations;

namespace StockPulse.Server.Entities
{
    public class Product : EntityBase
    {   
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string SKU { get; set; } = string.Empty;
    }
}

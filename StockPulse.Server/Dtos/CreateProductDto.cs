
using System.ComponentModel.DataAnnotations;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string SKU { get; set; } = string.Empty;

}
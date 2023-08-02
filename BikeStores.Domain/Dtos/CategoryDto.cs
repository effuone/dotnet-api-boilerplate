using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos;

public class CategoryDto {
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}

public class UpdateCategoryDto {
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
}

public class CreateCategoryDto {
    [Required]
    public string CategoryName { get; set; }
}
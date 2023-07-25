using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos.Brand;

public class CategoryDto {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
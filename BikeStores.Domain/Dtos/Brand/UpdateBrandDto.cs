using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos.Brand;

public class UpdateBrandDto {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
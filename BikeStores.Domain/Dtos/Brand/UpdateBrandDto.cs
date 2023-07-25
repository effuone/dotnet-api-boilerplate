using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos.Brand;

public class UpdateBrandDto {
    [Key]
    public int BrandId { get; set; }
    [Required]
    public string BrandName { get; set; }
}
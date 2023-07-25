using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos.Brand;

public class CreateBrandDto {
    [Required]
    public string Name { get; set; }
}
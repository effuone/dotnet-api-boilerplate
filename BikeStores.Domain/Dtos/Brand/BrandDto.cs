using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos.Brand;

public class BrandDto {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
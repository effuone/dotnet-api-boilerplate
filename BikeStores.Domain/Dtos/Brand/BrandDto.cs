using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos.Brand;

public class BrandDto {
    [Key]
    public int BrandId { get; set; }
    public string BrandName { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos;

public class BrandDto {
    [Key]
    public int BrandId { get; set; }
    public string BrandName { get; set; }
}

public class UpdateBrandDto {
    [Key]
    public int BrandId { get; set; }
    [Required]
    public string BrandName { get; set; }
}

public class CreateBrandDto {
    [Required]
    public string BrandName { get; set; }
}
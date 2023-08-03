using System.ComponentModel.DataAnnotations;

namespace BikeStores.Domain.Dtos;

public class BrandDto {
    [Key]
    public int BrandId { get; set; }
    public string BrandName { get; set; }
}

public class UpdateBrandDto {
    [Required]
    [MaxLength(20)]
    public string BrandName { get; set; }
}

public class CreateBrandDto {
    [Required]
    [MaxLength(20)]
    public string BrandName { get; set; }
}
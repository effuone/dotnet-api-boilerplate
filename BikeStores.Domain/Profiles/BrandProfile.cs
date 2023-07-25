using AutoMapper;
using BikeStores.Domain.Models;
using BikeStores.Domain.Dtos.Brand;

namespace BikeStores.Domain.Profiles;

public class BrandsProfile : Profile
{
    public BrandsProfile(){
        // Source ---> Target
        CreateMap<Brand, BrandDto>();
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();
    }
}

using AutoMapper;
using BikeStores.Domain.Models;
using BikeStores.Domain.Dtos;

namespace BikeStores.Domain.Profiles;

public class BrandsProfile : Profile
{
    public BrandsProfile(){
        CreateMap<Brand, BrandDto>();
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();
    }
}

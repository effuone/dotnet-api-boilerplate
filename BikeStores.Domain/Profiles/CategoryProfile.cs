using AutoMapper;
using BikeStores.Domain.Dtos;
using BikeStores.Domain.Models;

namespace BikeStores.Domain.Profiles;

public class CategoriesProfile : Profile
{
    public CategoriesProfile(){
        // Source ---> Target
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}

using Mapster;
using ProductManagement.Domain.Entities;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Category, CategoryDto>.NewConfig();

            TypeAdapterConfig<CategoryCreateUpdateDto, Category>.NewConfig()
                .ConstructUsing(dto => new Category(dto.Name, dto.Description));

            TypeAdapterConfig<Product, ProductDto>.NewConfig()
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : null);

            TypeAdapterConfig<ProductDto, Product>.NewConfig()
                .IgnoreNullValues(true);
        }
    }
}

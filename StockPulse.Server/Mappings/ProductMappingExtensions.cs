using StockPulse.Server.Entities;

namespace StockPulse.Server.Mappings
{
    public static class ProductMappingExtensions
    {
        public static Product ToEntity(this CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                SKU = dto.SKU
            };
        }

        //public static CreateProductDto ToDto(this Product entity)
        //{
        //    return new CreateProductDto
        //    {
        //        Name = entity.Name,
        //        SKU = entity.SKU
        //    };
        //}
    }
}

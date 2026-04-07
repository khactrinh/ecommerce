using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Catalog.GetProducts;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class ProductMapper
{
    public partial ProductDto ToDto(Product product);

    public partial List<ProductDto> ToDtoList(List<Product> products);
}
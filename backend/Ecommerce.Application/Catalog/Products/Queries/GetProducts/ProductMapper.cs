using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
using Ecommerce.Domain.Catalog.Products;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Catalog.GetProducts;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class ProductMapper
{
    public partial ProductListItemDto ToDto(Product product);

    public partial List<ProductListItemDto> ToDtoList(List<Product> products);
}
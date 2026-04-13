namespace Ecommerce.Application.Common.Interfaces;

public interface ICategoryQueryService
{
    Task<List<Guid>> GetDescendantIds(Guid categoryId);
}
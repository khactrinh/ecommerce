using Dapper;

namespace Ecommerce.Infrastructure.Persistence.Dapper;

public static class DapperConfig
{
    public static void Configure()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
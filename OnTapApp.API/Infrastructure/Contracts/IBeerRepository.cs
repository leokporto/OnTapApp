

using OnTapApp.Core.Entity;

namespace OnTapApp.API.Infrastructure.Contracts;

public interface IBeerRepository : IDbReadRepository<Beer>
{
    Task<IEnumerable<string>> GetBeerStyles();
}
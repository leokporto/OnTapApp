using System.Data;
using Dapper;
using OnTapApp.API.Infrastructure.Contracts;
using OnTapApp.Core.Entity;

namespace OnTapApp.API.Infrastructure.Repository;


public class BeerRepository : IBeerRepository
{
    protected readonly IDbConnection _connection;
    
    public BeerRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Beer?> GetByIdAsync(int id)
    {
        return await _connection.QuerySingleAsync<Beer?>("SELECT * FROM Beers WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Beer>> ListAsync()
    {
        return await _connection.QueryAsync<Beer>("SELECT * FROM Beers");
    }

    public async Task<IEnumerable<string>> GetBeerStyles()
    {
        return await _connection.QueryAsync<string>("SELECT distinct(style) FROM beers");
    }
}
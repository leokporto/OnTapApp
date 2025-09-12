using System.Data;
using Dapper;
using Npgsql;
using OnTapApp.API.Infrastructure.Contracts;
using OnTapApp.API.Infrastructure.Repository;

namespace OnTapApp.API.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;
    private BeerRepository? _beerRepository;

    public UnitOfWork(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);  // Ajuste para seu DB
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public IBeerRepository Beers => _beerRepository ??= new BeerRepository(_connection);

    public async Task<int> SaveChangesAsync()
    {
        if (_transaction == null) return 0;
        try
        {
            var rows = await Task.FromResult(_transaction.Connection!.Execute("COMMIT", null, _transaction, 0, CommandType.Text));
            _transaction.Dispose();
            _transaction = null;
            return rows;
        }
        catch
        {
            Rollback();
            throw;
        }
    }

    private void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection.Dispose();
    }
}
namespace OnTapApp.API.Infrastructure.Contracts;

public interface IUnitOfWork : IDisposable
{
    IBeerRepository Beers { get; }
    
    Task<int> SaveChangesAsync();  // Commit da transação
}
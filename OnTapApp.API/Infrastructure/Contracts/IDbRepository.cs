namespace OnTapApp.API.Infrastructure.Contracts;

public interface IDbReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> ListAsync();
    
}

public interface IDbWriteRepository<T> where T : class
{
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
}
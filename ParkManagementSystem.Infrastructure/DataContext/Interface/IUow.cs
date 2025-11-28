using Microsoft.EntityFrameworkCore;

namespace ParkManagementSystem.Infrastructure.DataContext.Interface;

public interface IUow
{
    DbContext Context { get; }
    void Commit();
    Task CommitAsync();
    Task CreateAsync<T>(T entity);
    void Update<T>(T entity);
    void Remove<T>(T entity);
    
}
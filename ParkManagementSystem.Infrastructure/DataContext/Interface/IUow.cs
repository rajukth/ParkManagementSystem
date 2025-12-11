using Microsoft.EntityFrameworkCore;

namespace ParkManagementSystem.Infrastructure.DataContext.Interface;

public interface IUow
{
    ApplicationDbContext  Context { get; }
    void SaveChanges();
    Task SaveChangesAsync();
    void Create<T>(T entity) where T : class;
    Task CreateAsync<T>(T entity) where T : class;
    void CreateMultiple<T>(IEnumerable<T> entities) where T : class;
    Task CreateMultipleAsync<T>(IEnumerable<T> entities) where T : class;
    void Remove<T>(T entity) where T : class;
    void RemoveMultiple<T>(IEnumerable<T> entities) where T : class;
}
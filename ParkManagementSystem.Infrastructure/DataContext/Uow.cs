using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.Infrastructure.DataContext.Interface;

namespace ParkManagementSystem.Infrastructure.DataContext;

public class Uow : IUow
{
    public ApplicationDbContext Context { get; }

    public Uow(ApplicationDbContext context)
    {
        Context = context;
    }

    public void SaveChanges() => Context.SaveChanges();
    public async Task SaveChangesAsync() =>await Context.SaveChangesAsync();

    public void Create<T>(T t) where T : class => Context.Add(t);
    public void CreateMultiple<T>(IEnumerable<T> entities) where T : class => Context.AddRange(entities);
    public async Task CreateAsync<T>(T entity) where T : class=> await Context.AddAsync(entity);
    public async Task CreateMultipleAsync<T>(IEnumerable<T> entities)  where T : class=> await Context.AddRangeAsync(entities);
    public void Remove<T>(T entity) where T : class=> Context.Remove(entity);
    public void RemoveMultiple<T>(IEnumerable<T> entities) where T : class => Context.RemoveRange(entities);
}
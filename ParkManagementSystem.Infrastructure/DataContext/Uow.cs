using Microsoft.EntityFrameworkCore;
using ParkManagementSystem.Infrastructure.DataContext.Interface;

namespace ParkManagementSystem.Infrastructure.DataContext;

public class Uow : IUow
{
    public DbContext Context { get; }

    public Uow(DbContext context)
    {
        Context = context;
    }

    public void Commit() => Context.SaveChanges();
    public async Task CommitAsync() =>await Context.SaveChangesAsync();

    public async Task CreateAsync<T>(T entity) => await Context.AddAsync(entity);

    public void Update<T>(T entity) => Context.Update(entity);

    public void Remove<T>(T entity) => Context.Remove(entity);
  
}
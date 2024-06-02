using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace ExamService.Infrastructure.Bases;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task DeleteRangeAsync(ICollection<T> entities);
    Task<T> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
    IDbContextTransaction BeginTransaction();
    void Commit();
    void RollBack();
    IQueryable<T> GetTableNoTracking();
    IQueryable<T> GetTableAsTracking();
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(ICollection<T> entities);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, string[] includes = null);
    Task<T?> FindAsync(Expression<Func<T, bool>> expression, string[] includes = null);

}

using Blazor2App.Database.Entities;
using BlazorApp2.Appllication.Models;
using System.Linq.Expressions;

namespace Blazor2App.Repository.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        Task<int> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> UpdateAsync(T entity);

        Task<PagedList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken cancellationToken = default
        );
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includes);

        IQueryable<T> GetQuery(Func<IQueryable<T>, IQueryable<T>>? includes = null, bool asNoTracking = false);
    }
}
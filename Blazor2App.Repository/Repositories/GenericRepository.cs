using Blazor2App.Database.Base;
using Blazor2App.Database.Entities;
using BlazorApp2.Appllication.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blazor2App.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly DataContext _context;

        protected GenericRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Set<T>().Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<PagedList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            IList<Expression<Func<T, object>>>? includes = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken cancellationToken = default
            )
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            var count = query.Count();

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            var entities = await query.ToListAsync(cancellationToken);
            return new PagedList<T>
            {
                Page = page,
                PageSize = pageSize,
                Count = count,
                Data = entities
            };
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<T> GetQuery(Func<IQueryable<T>, IQueryable<T>>? includes, bool asNoTracking = false)
        {

            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
                query = includes(query);
            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }
}

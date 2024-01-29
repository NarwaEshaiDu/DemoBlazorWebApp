using Microsoft.EntityFrameworkCore;

namespace Blazor2App.Database.Entities
{
    public interface IDatabaseContext
    {
        /// <summary>
        /// Access db context info for additional functionality
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// Commit changes to database.
        /// </summary>
        /// <param name="token">Cancellation token in case process is interrupted.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken token = default);

        /// <summary>
        /// 
        /// </summary>
        DbSet<StudentEntity> StudentEntities { get; set; }
    }
}


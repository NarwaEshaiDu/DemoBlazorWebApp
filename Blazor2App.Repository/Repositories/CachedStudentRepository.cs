using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Blazor2App.Repository.Repositories
{
    public class CachedStudentRepository : IStudentRepository
    {
        private readonly StudentRepository _studentRepository;
        private readonly IMemoryCache _memoryCache;
        private static readonly SemaphoreSlim semaphore = new(1, 5);

        public CachedStudentRepository(StudentRepository studentRepository, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _studentRepository = studentRepository;
        }
        public async Task<IEnumerable<StudentModel>?> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            const string key = $"studentsCacheKey";
            await semaphore.WaitAsync(cancellationToken);

            if (_memoryCache.TryGetValue(key, out IEnumerable<StudentModel>? students))
            {
                await Console.Out.WriteLineAsync("Student list still populated.");
                return students;
            }
            else
            {
                await Console.Out.WriteLineAsync("student list not populated");
                semaphore.Release();

                try
                {
                    return await _memoryCache.GetOrCreateAsync(
                        key,
                async entry =>
                    {
                        entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(40));
                        entry.SetSlidingExpiration(TimeSpan.FromSeconds(3600));
                        entry.SetPriority(CacheItemPriority.Normal);
                        entry.SetSize(1024);

                        return await _studentRepository.GetAllStudentsAsync(cancellationToken)!;
                    });
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }

        public async Task<StudentModel?> GetStudentByIdAsync(int id, CancellationToken cancellationToken)
        {
            string key = $"student-{id}";

            return await _memoryCache.GetOrCreateAsync(
                key,
                async entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                    return await _studentRepository.GetStudentByIdAsync(id, cancellationToken)!;
                });
        }

        public async Task<int> SaveAsync(StudentModel studentModel, CancellationToken cancellationToken)
        {
            _memoryCache.Remove("studentsCacheKey");
            return await _studentRepository.SaveAsync(studentModel, cancellationToken);
        }

        public async Task<int> UpdateAsync(StudentModel studentModel, CancellationToken cancellationToken)
        {
            return await _studentRepository.UpdateAsync(studentModel, cancellationToken);
        }
    }
}

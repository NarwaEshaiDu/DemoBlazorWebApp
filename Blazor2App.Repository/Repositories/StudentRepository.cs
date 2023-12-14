using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using Blazor2App.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazor2App.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDatabaseContext _dbContext;
        public StudentRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            var students = await _dbContext.StudentEntities.AsNoTracking().ToListAsync(cancellationToken);

            return students.Select(StudentEntity.ToModel);
        }

        public async Task<int> UpdateAsync(StudentModel studentModel, CancellationToken cancellationToken)
        {
            await _dbContext.StudentEntities.SingleUpdateAsync(StudentEntity.FromModelToEntity(studentModel), cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> SaveAsync(StudentModel student, CancellationToken cancellationToken)
        {
            await _dbContext.StudentEntities.AddAsync(StudentEntity.FromModelToEntity(student), cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

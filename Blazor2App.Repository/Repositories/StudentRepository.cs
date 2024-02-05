using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using Blazor2App.Database.Base;
using Blazor2App.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazor2App.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _dbContext;
        public StudentRepository(DataContext dbContext)
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
            var entity = StudentEntity.FromModelToEntity(student);
            await _dbContext.StudentEntities.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

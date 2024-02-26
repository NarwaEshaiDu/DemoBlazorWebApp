using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using Blazor2App.Database.Base;
using Blazor2App.Database.Entities;
using MassTransit.Initializers;
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
            return await _dbContext.StudentEntities.AsNoTracking()
                   .Select(e => new StudentModel
                   {
                       Id = e.Id,
                       LastModified = e.LastModified,
                       Name = e.Name
                   })
                   .ToListAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(StudentModel studentModel, CancellationToken cancellationToken)
        {
            await _dbContext.StudentEntities.SingleUpdateAsync(StudentEntity.FromModelToEntity(studentModel), cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveAsync(StudentModel student, CancellationToken cancellationToken)
        {
            var entity = StudentEntity.FromModelToEntity(student);
            entity.LastModified = DateTime.UtcNow;

            await _dbContext.StudentEntities.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<StudentModel> GetStudentByIdAsync(int id, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Set<StudentEntity>()
                .FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

            return StudentEntity.ToModel(student);
        }
    }
}

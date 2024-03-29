﻿using Blazor2App.Application.Models;

namespace Blazor2App.Application.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentModel>> GetAllStudentsAsync(CancellationToken cancellationToken);
        Task<int> UpdateAsync(StudentModel studentModel, CancellationToken cancellationToken);
        Task<int> SaveAsync(StudentModel studentModel, CancellationToken cancellationToken);
        Task<StudentModel> GetStudentByIdAsync(int id, CancellationToken cancellationToken);
    }
}

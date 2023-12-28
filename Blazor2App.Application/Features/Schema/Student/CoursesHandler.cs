using Blazor2App.Application.Features.Schema.DTO;
using Bogus;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Student
{
    public class CoursesHandler : IRequestHandler<CreateCourseCommand, CourseModel>, IRequestHandler<GetCourseById, CourseModel>, IRequestHandler<GetCourses, List<CourseModel>>
    {
        private Faker<CourseModel> _coursesFaker;
        private Faker<InstructorModel> _instructorsFaker;
        public CoursesHandler()
        {
            _coursesFaker = new Faker<CourseModel>();
            _instructorsFaker = new Faker<InstructorModel>();
        }

        public Task<List<CourseModel>> Handle(GetCourses request, CancellationToken cancellationToken)
        {
            return Task.FromResult(GenerateData().ToList());
        }

        public Task<CourseModel> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CourseModel> Handle(GetCourseById request, CancellationToken cancellationToken)
        {
            var record = GenerateData().First();
            record.Id = request.Id;

            return Task.FromResult(record);
        }

        private IEnumerable<CourseModel> GenerateData() 
        {
            _instructorsFaker = new Faker<InstructorModel>()
              .RuleFor(c => c.Id, f => Guid.NewGuid())
              .RuleFor(c => c.FirstName, f => f.Name.FirstName())
              .RuleFor(c => c.LastName, f => f.Name.LastName())
              .RuleFor(c => c.Salary, f => f.Random.Int(2000, 4000));

            _coursesFaker = new Faker<CourseModel>()
                .RuleFor(c => c.Id, f => f.UniqueIndex)
                .RuleFor(c => c.Title, f => f.Name.JobTitle())
                .RuleFor(c => c.IsCompleted, f => f.Random.Bool())
                .RuleFor(c => c.Instructors, f => _instructorsFaker.Generate(1));

            return _coursesFaker.Generate(4);
        }
    }
}

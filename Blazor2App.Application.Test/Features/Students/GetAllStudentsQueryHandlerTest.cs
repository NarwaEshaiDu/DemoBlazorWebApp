using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Repositories;
using Blazor2App.Application.Test.Builders.Students;
using Blazor2App.Shared.Builder;
using Moq;

namespace Blazor2App.Application.Test.Features.Students
{
    public class GetAllStudentsQueryHandlerTest
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly GetAllStudentsQueryHandler _sut;
        private readonly IBuilder<GetAllStudentsQuery> _query;

        public GetAllStudentsQueryHandlerTest()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _query = new GetAllStudentsQueryBuilder();
            _sut = new GetAllStudentsQueryHandler(_studentRepository.Object);
        }

        [Fact]
        public async Task GetAllStudents_Success()
        {
            //Arrange 
            var query = _query.Build();

            //Act
            var sut = await _sut.Handle(query, default);

            //Assert
            Assert.NotNull(sut);

        }
    }
}

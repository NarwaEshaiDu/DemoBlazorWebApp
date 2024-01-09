using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Test.Builders.Students;
using Blazor2App.Shared.Builder;

namespace Blazor2App.Application.Test.Features.Students
{
    public class GetAllStudentwQueryValidatorTest
    {
        private readonly GetAllStudentsQueryValidator _sut;
        private readonly IBuilder<GetAllStudentsQuery> _query;

        public GetAllStudentwQueryValidatorTest()
        {
            _query = new GetAllStudentsQueryBuilder();
            _sut = new GetAllStudentsQueryValidator();
        }

        [Fact]
        public async Task GetAllStudents_Success()
        {
            //Arrange
            var query = _query.Build();

            //Act
            var sut = await _sut.ValidateAsync(query, default);

            //Assert    
            Assert.NotNull(sut);
        }
    }
}

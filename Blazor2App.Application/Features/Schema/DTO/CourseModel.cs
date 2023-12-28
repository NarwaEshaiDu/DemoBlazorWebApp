namespace Blazor2App.Application.Features.Schema.DTO
{
    public class CourseModel
    {
        public int Id { get; set; }
        [GraphQLName("title")]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        [GraphQLNonNullType]
        public IEnumerable<InstructorModel> Instructors { get; set; }
    }
}

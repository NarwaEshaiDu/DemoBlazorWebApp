using Blazor2App.Application.Features.Schema.DTO;
using Blazor2App.Application.Features.Schema.Queries;
using Blazor2App.Application.Models;

namespace Blazor2App.Server
{
    public partial class DependencyInjection
    {
        public static void RegisterGraphQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGraphQLServer()
              .AddQueryType<Query>()
              .AddTypeExtension<QueryCourses>()
              .AddTypeExtension<QueryStudents>()
              .AddType<CourseModel>()
              .AddType<InstructorModel>()
              .AddType<StudentModel>()
              .AddAuthorization();
        }
    }
}

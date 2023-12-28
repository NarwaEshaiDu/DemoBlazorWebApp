using Blazor2App.Application.Features.Schema.Mutations;
using Blazor2App.Application.Features.Schema.Queries;

namespace Blazor2App.Server
{
    public partial class DependencyInjection
    {
        public static void RegisterGraphQL(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRouting();

            services
                .AddGraphQLServer()
                .AddQueryType<QueryCourses>()
                .AddMutationType<Mutation>();


            //TODO: make dynamic like dbcontext
            services
                .AddGraphQLServer("studentSchema")
                .AddQueryType<QueryStudents>();
        }
    }
}

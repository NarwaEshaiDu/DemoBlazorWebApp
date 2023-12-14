using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor2App.Application.Features.Students.Queries
{
    public class GetAllStudentsQueryValidator : IRequest<GetAllStudentsQuery>
    {
    }
}

using Blazor2App.Application.Features.Schema.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor2App.Application.Features.Schema.Student
{
    public class GetTodosQuery : IRequest<List<Todo>>
    {
    }

}

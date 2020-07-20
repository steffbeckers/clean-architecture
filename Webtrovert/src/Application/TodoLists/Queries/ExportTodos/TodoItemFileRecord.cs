using Webtrovert.Application.Common.Mappings;
using Webtrovert.Domain.Entities;

namespace Webtrovert.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}

using SteffBeckers.Application.Common.Mappings;
using SteffBeckers.Domain.Entities;

namespace SteffBeckers.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}

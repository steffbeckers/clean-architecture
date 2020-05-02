using SteffBeckers.Application.Common.Mappings;
using SteffBeckers.Domain.Entities;
using System.Collections.Generic;

namespace SteffBeckers.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public IList<TodoItemDto> Items { get; set; }
}
}

using System.Collections.Generic;
using Core.Models;

namespace Domain.IRepository
{
    public interface ITodoRepository
    {
        TodoItem CreateTodoItem(TodoItem item);
        List<TodoItem> getAllTodoItems();
        TodoItem GetTodoItemById(int id);
        bool DeleteTodoItem(int id);
        bool UpdateTodoItem(TodoItem item);
    }
}
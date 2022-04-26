using System.Collections.Generic;
using Core.Models;

namespace Core.IServices
{
    public interface ITodoService
    {
        TodoItem CreateTodoItem(TodoItem item);
        List<TodoItem> GetAllTodoItems();
        TodoItem GetTodoItemById(int id);
        bool DeleteTodoItem(int id);
        bool UpdateTodoItem(TodoItem item);
    }
}
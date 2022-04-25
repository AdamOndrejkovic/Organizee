using System.Collections.Generic;
using System.Data;
using Core.IServices;
using Core.Models;
using Domain.IRepository;

namespace Domain.Service
{
    public class TodoService: ITodoService
    {

        private readonly ITodoRepository _todoRepository;
        
        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository ?? throw new NoNullAllowedException("Todo repository can not be null"); 
        }

        public TodoItem CreateTodoItem(TodoItem item)
        {
            return _todoRepository.CreateTodoItem(item);
        }

        public List<TodoItem> getAllTodoItems()
        {
            return _todoRepository.getAllTodoItems();
        }

        public TodoItem GetTodoItemById(int id)
        {
            return _todoRepository.GetTodoItemById(id);
        }

        public bool DeleteTodoItem(int id)
        {
            return _todoRepository.DeleteTodoItem(id);
        }

        public bool UpdateTodoItem(TodoItem item)
        {
            return _todoRepository.UpdateTodoItem(item);
        }
    }
}
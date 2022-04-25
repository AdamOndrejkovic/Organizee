using System.Collections.Generic;
using System.Linq;
using Core.Models;
using DataAccess.Entities;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TodoRepository: ITodoRepository
    {

        private readonly OrganizeeDbContext _organizeeDb;

        public TodoRepository(OrganizeeDbContext organizeeDb)
        {
            _organizeeDb = organizeeDb;
        }


        public TodoItem CreateTodoItem(TodoItem item)
        {
            var todoItem = new TodoItemEntity()
            {
                UserId = item.UserId,
                Title = item.Title,
                Description = item.Description,
                Complete = item.Complete,
            };

            _organizeeDb.Todos.Attach(todoItem).State = EntityState.Added;
            _organizeeDb.SaveChanges();
            return item;
        }

        public List<TodoItem> getAllTodoItems()
        {
            return _organizeeDb.Todos.Select(todo => new TodoItem()
                {
                    Id = todo.Id,
                    UserId = todo.UserId,
                    Title = todo.Title,
                    Description = todo.Description,
                    Complete = todo.Complete,
                }
            ).ToList();
        }

        public TodoItem GetTodoItemById(int id)
        {
            var todoItemById = _organizeeDb.Todos.FirstOrDefault(todo => id.Equals(todo.Id));
            if (todoItemById != null)
            {
                return new TodoItem()
                {
                    Id = todoItemById.Id,
                    UserId = todoItemById.UserId,
                    Title = todoItemById.Title,
                    Description = todoItemById.Description,
                    Complete = todoItemById.Complete,
                };
            }

            return null;
        }

        public bool DeleteTodoItem(int id)
        {
            var todoItemToRemove = _organizeeDb.Todos.Where(todo => id.Equals(todo.Id));
            if (todoItemToRemove == null) return false;
            _organizeeDb.RemoveRange(todoItemToRemove);
            _organizeeDb.SaveChanges();
            return true;

        }

        public bool UpdateTodoItem(TodoItem item)
        {
            var todoItemToUpdate = new TodoItemEntity()
            {
                Id = item.Id,
                UserId = item.UserId,
                Title = item.Title,
                Description = item.Description,
                Complete = item.Complete,
            };
            if (todoItemToUpdate == null) return false;
            _organizeeDb.Update(todoItemToUpdate);
            _organizeeDb.SaveChanges();
            return true;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.Dto;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController
    {

        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public ActionResult<TodoDto> CreateTodo([FromBody] TodoDto todoDto)
        {
            var createTodo = _todoService.CreateTodoItem(new TodoItem()
            {
                UserId = todoDto.UserId,
                Title = todoDto.Title,
                Description = todoDto.Description
            });

            if (createTodo == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new TodoDto()
            {
                Id = createTodo.Id,
                UserId = createTodo.UserId,
                Title = createTodo.Title,
                Description = createTodo.Description,
                Complete = createTodo.Complete
            };
        }

        [HttpGet]
        public ActionResult<List<TodoDto>> GetAllTodos()
        {
            return _todoService.GetAllTodoItems().Select(todo => new TodoDto()
            {
                Id = todo.Id,
                UserId = todo.UserId,
                Title = todo.Title,
                Description = todo.Description,
                Complete = todo.Complete,
            }).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TodoDto> GetTodoById(int id)
        {
            var todoById = _todoService.GetTodoItemById(id);
            if (todoById == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new TodoDto()
            {
                Id = todoById.Id,
                UserId = todoById.UserId,
                Title = todoById.Title,
                Description = todoById.Description,
                Complete = todoById.Complete
            };
        }

        [HttpPut]
        public ActionResult<TodoDto> UpdateTodo([FromBody] TodoDto todoDto)
        {
            var updateTodo = _todoService.UpdateTodoItem(new TodoItem()
            {
                Id = todoDto.Id,
                UserId = todoDto.UserId,
                Title = todoDto.Title,
                Description = todoDto.Description,
                Complete = todoDto.Complete,
            });

            return updateTodo ? new StatusCodeResult(StatusCodes.Status202Accepted) : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoDto> DeleteTodo(int id)
        {
            var removeTodo = _todoService.DeleteTodoItem(id);
            
            return removeTodo ? new StatusCodeResult(StatusCodes.Status202Accepted) : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
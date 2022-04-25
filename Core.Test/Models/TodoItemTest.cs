using Core.Models;
using Xunit;

namespace Core.Test.Models
{
    public class TodoItemTest
    {
        private readonly TodoItem _todo;

        public TodoItemTest()
        {
            _todo = new TodoItem();
        }

        [Fact]
        public void TodoItemClass_Exists()
        {
            Assert.NotNull(_todo);
        }
        
        [Fact]
        public void TodoItemClass_HasId_WithTypeInt()
        {
            var expected = 1;
            _todo.Id = 1;
            Assert.Equal(expected, _todo.Id);
            Assert.True(_todo.Id is int);
        }
        
        [Fact]
        public void TodoItemClass_HasUserId_WithTypeInt()
        {
            var expected = 1;
            _todo.UserId = 1;
            Assert.Equal(expected, _todo.UserId);
            Assert.True(_todo.UserId is int);
        }
        
        [Fact]
        public void TodoItemClass_HasTitle_WithTypeString()
        {
            var expected = "Title";
            _todo.Title = "Title";
            Assert.Equal(expected, _todo.Title);
            Assert.True(_todo.Title is string);
        }
        
        [Fact]
        public void TodoItemClass_HasDescription_WithTypeString()
        {
            var expected = "Description";
            _todo.Description = "Description";
            Assert.Equal(expected, _todo.Description);
            Assert.True(_todo.Description is string);
        }
        
        [Fact]
        public void TodoItemClass_HasComplete_WithTypeBoolean()
        {
            var expected = true;
            _todo.Complete = true;
            Assert.Equal(expected, _todo.Complete);
            Assert.True(_todo.Complete is bool);
        }
    }
}
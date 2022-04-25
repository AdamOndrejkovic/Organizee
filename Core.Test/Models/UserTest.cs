using Core.Models;
using Xunit;

namespace Core.Test.Models
{
    public class UserTest
    {
        private readonly User _user;

        public UserTest()
        {
            _user = new User();
        }

        [Fact]
        public void UserClass_Exists()
        {
            Assert.NotNull(_user);
        }
        
        [Fact]
        public void UserClass_HasId_WithTypeInt()
        {
            var expected = 1;
            _user.Id = 1;
            Assert.Equal(expected, _user.Id);
            Assert.True(_user.Id is int);
        }
        
        [Fact]
        public void UserClass_HasName_WithTypeString()
        {
            var expected = "Cool name";
            _user.Name = "Cool name";
            Assert.Equal(expected, _user.Name);
            Assert.True(_user.Name is string);
        }

        
        [Fact]
        public void UserClass_HasHashedPassword_WithTypeString()
        {
            var expected = "Top Secret Password";
            _user.HashedPassword = "Top Secret Password";
            Assert.Equal(expected, _user.HashedPassword);
            Assert.True(_user.HashedPassword is string);
        }
    }
}
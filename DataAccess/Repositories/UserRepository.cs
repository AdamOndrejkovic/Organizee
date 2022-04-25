using System.Data;
using System.Linq;
using Core.Models;
using DataAccess.Entities;
using Domain.IRepository;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrganizeeDbContext _organizeeDb;

        public UserRepository(OrganizeeDbContext organizeeDb)
        {
            _organizeeDb = organizeeDb ??
                           throw new NoNullAllowedException("Organizee database context can not be null");
        }

        public User GetUserById(int id)
        {
            var userById = _organizeeDb.Users.FirstOrDefault(user => id.Equals(user.Id));
            if (userById != null)
            {
                return new User()
                {
                    Id = userById.Id,
                    Name = userById.Name,
                    HashedPassword = userById.HashedPassword,
                    /*Salt to byte array*/
                };
            }

            return null;
        }

        public bool DeleteUser(int id)
        {
            var userToRemove = _organizeeDb.Users.Where(user => id.Equals(user.Id));
            if (userToRemove == null) return false;
            _organizeeDb.RemoveRange(userToRemove);
            _organizeeDb.SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            var userToUpdate = new UserEntity()
            {
                Id = user.Id,
                Name = user.Name,
                HashedPassword = user.HashedPassword,
                Salt = user.Salt.ToString()
            };
            if (userToUpdate == null) return false;
            _organizeeDb.Update(userToUpdate);
            _organizeeDb.SaveChanges();
            return true;
        }
    }
}
using System.Data;
using System.Linq;
using System.Text;
using Core.Models;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Security.Repository
{
    public class AuthRepository: IAuthRepository
    {

        private readonly OrganizeeDbContext _organizeeDb;

        public AuthRepository(OrganizeeDbContext organizeeDb)
        {
            _organizeeDb = organizeeDb;
        }

        public User GetUser(string name)
        {
            var userByName = _organizeeDb.Users.FirstOrDefault(user => name.Equals(user.Name));
            if (userByName != null)
            {
                return new User()
                {
                    Id = userByName.Id,
                    Name = userByName.Name,
                    HashedPassword = userByName.HashedPassword,
                    Salt = Encoding.ASCII.GetBytes(userByName.Salt),
                };
            }

            return null;
        }

        public User RegisterUser(string name, string hashedPassword, string salt)
        {
            if (GetUser(name) != null)
            {
                throw new DataException("User with the name: " + name + " already exists");
            }
            var user = new UserEntity()
            {
                Name = name,
                HashedPassword = hashedPassword,
                Salt = salt
            };

            _organizeeDb.Users.Attach(user).State = EntityState.Added;
            _organizeeDb.SaveChanges();
            return new User()
            {
                Name = name,
                HashedPassword = hashedPassword,
            };
        }
    }
}
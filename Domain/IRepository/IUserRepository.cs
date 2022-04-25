using Core.Models;

namespace Domain.IRepository
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        bool DeleteUser(int id);
        bool UpdateUser(User user);
    }
}
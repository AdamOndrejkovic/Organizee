using Core.Models;

namespace Core.IServices
{
    public interface IUserService
    {
        User GetUserById(int id);
        bool DeleteUser(int id);
        bool UpdateUser(User user);
    }
}
using Core.Models;

namespace Security.Repository
{
    public interface IAuthRepository
    {
        User GetUser(string name);
        User RegisterUser(string name, string hashedPassword, string salt);
    }
}
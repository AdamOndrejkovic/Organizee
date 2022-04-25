using Core.Models;

namespace Security.IServices
{
    public interface IAuthService
    {
        User GetUser(string name);
        User RegisterUser(string name, string hashedPassword, string salt);
    }
}
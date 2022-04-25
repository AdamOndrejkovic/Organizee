using Core.Models;

namespace Security.IServices
{
    public interface ISecurityService
    {
        string HashedPassword(string plainTextPassword, byte[] userSalt);
        User RegisterUser(string name, string password);
        User Login(string name, string password);
    }
}
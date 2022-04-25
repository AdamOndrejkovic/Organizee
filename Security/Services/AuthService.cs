using System.Data;
using Core.Models;
using Security.IServices;
using Security.Repository;

namespace Security.Services
{
    public class AuthService: IAuthService
    {

        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository ?? throw new NoNullAllowedException("Auth repository can not be null.");
        }

        public User GetUser(string name)
        {
            return _authRepository.GetUser(name);
        }

        public User RegisterUser(string name, string hashedPassword, string salt)
        {
            return _authRepository.RegisterUser(name, hashedPassword, salt);
        }
    }
}
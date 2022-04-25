using System;
using System.Security.Authentication;
using System.Text;
using Core.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Security.IServices;

namespace Security.Services
{
    public class SecurityService: ISecurityService
    {
        private readonly IAuthService _authService;

        public SecurityService(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        
        private IConfiguration Configuration { get; }

        public string HashedPassword(string plainTextPassword, byte[] userSalt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainTextPassword,
                salt: userSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public User RegisterUser(string name, string password)
        {
            var salt = "123#$%^";
            var hashedPassword =HashedPassword(password,Encoding.ASCII.GetBytes(salt));

            return _authService.RegisterUser(name, hashedPassword, salt);
        }

        public User Login(string name, string password)
        {
            var user = _authService.GetUser(name);

            if (!Authenticate(password,user))
            {
                throw new AuthenticationException("Email or Password not correct");
            }

            return user;
        }
        
        private bool Authenticate(string plainTextPassword, User user)
        {
            if (user == null || user.HashedPassword.Length <= 0 || user.Salt.Length <=0)
            {
                return false;
            }
            

            var hashedPasswordFromPlain = HashedPassword(plainTextPassword,user.Salt);

            return user.HashedPassword.Equals(hashedPasswordFromPlain);
        }
    }
}
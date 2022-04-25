using System.Data;
using Core.IServices;
using Core.Models;
using Domain.IRepository;

namespace Domain.Service
{
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new NoNullAllowedException("User repository can not be null");
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }
    }
}
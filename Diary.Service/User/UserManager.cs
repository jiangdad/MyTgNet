using Diary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Service
{
    class UserManager : IUserManager
    {
        IUserRepository _IUserRepository;
        public UserManager(IUserRepository userRepository)
        {
            _IUserRepository = userRepository;
        }


        public IUserService add(User user)
        {
            var md5 = new Tgnet.Security.MD5();
            user.Password = md5.Encrypt(user.Password);
            _IUserRepository.Add(user);
            _IUserRepository.SaveChanges();
            IUserService userService = new UserService(_IUserRepository, user.UserId);//返回User user
            return userService;
        }


        bool IUserManager.CheckUserName(string username)
        {
            return _IUserRepository.Entities.Any(user =>
                user.UserName == username
            );
        }

        IUserService IUserManager.GetService(User user)
        {
            throw new NotImplementedException();
        }
    }
}

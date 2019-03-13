using Diary.Data;
using Diary.Service.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgnet;

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
            ExceptionHelper.ThrowIfNull(user, "用户", "用户对象为空");
            var md5 = new Tgnet.Security.MD5();
            user.Password = md5.Encrypt(user.Password);
            _IUserRepository.Add(user);
            _IUserRepository.SaveChanges();
            IUserService userService = new UserService(_IUserRepository, user.UserId);//返回User user
            return userService;
        }

        //IUserService IUserManager.add(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //IUserService IUserManager.add(User user)
        //{
        //    throw new NotImplementedException();
        //}

        bool IUserManager.CheckUserName(string username)
        {
            ExceptionHelper.ThrowIfNullOrEmpty(username, "用户名", "用户名无效");
            return _IUserRepository.Entities.Any(user =>
                user.UserName == username
            );
        }

        IUserService IUserManager.GetService(int userid)
        {
            ExceptionHelper.ThrowIfNotId(userid, "用户id", "用户ID无效");
            return new UserService( _IUserRepository,userid);
        }
       
        //核查登陆密码和用户名是否正确（即在UserRepository仓储类中是否存在）
        LoginUserModel IUserManager.Login(User user)
        {
            //1 实例化LoginUserModel对象
            //2 把Password转化为加密形式
            //3 在User仓储类找到用户名和密码 IsDel为false的实体
            //4.1没找到 返回error登陆模型
            //4.2找到返回ok登陆模型.更新User仓储类里面的landIp,lastLandTime属性
            ExceptionHelper.ThrowIfNull(user, "登陆用户", "登陆用户无效");
            LoginUserModel model = new LoginUserModel();
            var md5 = new Tgnet.Security.MD5();
            user.Password = md5.Encrypt(user.Password);
            var userEntity = _IUserRepository.Entities.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if (userEntity == null)
            {
                model.Status = "error";
                model.Msg = "对不起，用户名与密码不正确";
                return model;
            }

            //登陆模型赋值
            model.Status = "ok";
            model.Msg = "登陆成功";
            model.UserId = userEntity.UserId;
            model.UserName = userEntity.UserName;
            //找到仓储类中userid等于 userEntity.userId的实体把它的landIp，lastLandTime更新
            return model;
        }
    }
}

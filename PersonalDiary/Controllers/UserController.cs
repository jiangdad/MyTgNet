using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.Service;
using Tgnet;
using PersonalDiary.Models;
using Tgnet.Web.Mvc;
using Diary.Data;

namespace PersonalDiary.Controllers
{
    public class UserController : BaseController
    {
        public IUserManager _UserManager;
        public UserController(IUserManager usermanager)
        {
            _UserManager = usermanager;
        }
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "UserName,Password")]User user)
        {
            //bool flag=_UserManager.CheckUserName(user.UserName);
            //if(flag)
            //{
            //    IUserService _UserService=_UserManager.add(user);

            //}
            string password = user.Password;
            string msg = "";
            if (!StringRule.VerifyPassword(password))
            {
                msg = "密码长度不符合规范";
                return JsonString(new BaseReponseModel() { Msg = msg,Status="no", url="User/Register" });//JsonString return JsonStringResult(object value) 
            }
            if (string.IsNullOrEmpty(user.UserName) || _UserManager.CheckUserName(user.UserName))
            {
                msg = "用户名为空或该用户已经存在";
                return JsonString(new BaseReponseModel() { Msg = msg, Status = "no", url = "User/Register" });
            }
          var userservice= _UserManager.add(user);
            SaveLoginUser(new LoginUserModel() { UserId = userservice.Id });
            return JsonString(new BaseReponseModel() { Msg = "注册成功",Status = "ok", url = "User/Login" });

        }
        private void SaveLoginUser(LoginUserModel userModel)
        {
            SessionService.SetCurrentUser(userModel.UserId, userModel.UserId.ToString(), false);
        }
    }
}
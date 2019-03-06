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
        //为什么写这个构造函数
        public UserController(IUserManager usermanager)
        {
            _UserManager = usermanager;
        }
        // [HttpPost]请求登陆控制器
        [HttpPost]
        public ActionResult Login([Bind(Include = "userName,passWord")]User user)
        {
            //1.判断SessionUser类属性User是否为空
            //1.1为空
            //1.1.1判断view post传过来的后绑定的user是否为空
            //1.1.1.1 user对象为空，返回Msg="请输入用户名"的回应模型JsonStringResult对象
            //1.1.1.2 user对象不为null,调用IUserManager.Login()方法,返回登陆模型LoginModel,调用SaveLoginUser方法,返回JsonString(model)
            //1.2不为空，用户已经登陆，跳转首页
            //清空Session
            //SessionService.ClearCurrentUser();
            if (User != null)
            {//已经登陆了，跳转首页
                return JsonString(new BaseReponseModel
                {
                    Msg = "用户已经登陆",
                    Status = "ok",
                    Url = Url.RouteUrl(new { controller = "Diary", action = "Index", userid = User.ID }
                    )
                });
            }
            if (user == null)
            {
                return JsonString(new BaseReponseModel { Msg = "请输入用户名", Status = "error" ,Url=Url.RouteUrl(new {
                    controller ="User",
                 action="Login"
                   })});
            }
            var model = _UserManager.Login(user);
            if (model.UserId > 0)
            {
                SaveLoginUser(model);
                return JsonString(new BaseReponseModel { Msg = "登陆成功", Status = "ok", Url = Url.RouteUrl(new { controller = "Diary", action = "Index" ,userid=model.UserId}) });
            }
            else
                return JsonString(new BaseReponseModel { Msg = "登陆失败", Status = "no", Url = Url.RouteUrl(new { controller = "User", action = "Login" }) });
        }
        //Get登陆控制Action
        public ActionResult Login()
        {
            return View();
        }
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "userName,password")]User user)
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
                return JsonString(new BaseReponseModel() { Msg = msg,Status="no", Url = Url.RouteUrl(new { controller = "Home", action = "Index" }) });//JsonString return JsonStringResult(object value) 
            }
            if (string.IsNullOrEmpty(user.UserName) || _UserManager.CheckUserName(user.UserName))
            {
                msg = "用户名为空或该用户已经存在";
                return JsonString(new BaseReponseModel() { Msg = msg, Status = "no", Url = Url.RouteUrl(new { controller = "Home", action = "Index" }) });
            }
            var userservice= _UserManager.add(user);
            SaveLoginUser(new LoginUserModel() { UserId = userservice.UserId });
            return JsonString(new BaseReponseModel() { Msg = "注册成功",Status = "ok", Url = Url.RouteUrl(new { controller = "Home", action = "Index" }) });

        }
        [HttpPost]
        public ActionResult AddUser([Bind(Include = "userName,password")]User user)
        {
           //1 合法性检查
           //2 不能有重复用户名
           //3 
            var userservice = _UserManager.add(user);
            
            return View("UserAddResult",user);
        }
        //保存登陆用户，调用 SessionService属性的SetCurrentUserf方法返回一个SessionUser类对象（有值，来自登陆模型）
        private void SaveLoginUser(LoginUserModel userModel)
        {
            SessionService.SetCurrentUser(userModel.UserId, userModel.UserId.ToString(), false);
        }
    }
}
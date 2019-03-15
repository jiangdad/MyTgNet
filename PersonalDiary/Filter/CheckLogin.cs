using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tgnet.Web.Mvc;
using Tgnet.Web.Mvc.Sessions;
using Tgnet.Web.Sessions;

namespace PersonalDiary.Filter
{
    public class CheckLogin : Tgnet.Web.Mvc.AuthorizeAttribute
    {
        //验证登陆
        //AuthorizeCore返回失败后执行
        protected override ActionResult UnauthorizedResult
        {
            get
            {
                RedirectResult LoginUrl = new RedirectResult("/User/Login");
                return LoginUrl;
            }
        }


        //参数 获取SessionUser参数和 httpContext参数，判断user是否为空，为空运行UnauthorizedResult属性
        protected override bool AuthorizeCore(HttpContextBase httpContext, SessionUser user)
        {
            if (user == null)
                throw new Tgnet.Api.ExceptionWithErrorCode(Tgnet.Api.ErrorCode.未登录);
            return true;
        }
    }
}
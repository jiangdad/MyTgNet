using Newtonsoft.Json;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tgnet.Api;

namespace PersonalDiary.Filter
{
    public class CustomHandletErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.Clear();
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                HandleAjaxException(filterContext);
            }
            base.OnException(filterContext);

        }

        private void HandleAjaxException(ExceptionContext filterContext)
        {
            var errorCode = filterContext.Exception as ExceptionWithErrorCode;
            BaseReponseModel model = new BaseReponseModel() { Status = "error" };
            if (errorCode.ErrorCode.Code == ErrorCode.未登录.Code)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                model.Url = UrlHelper.GetVirtualPath("User", "Login");
                filterContext.HttpContext.Response.Redirect(model.Url, true);

            }
            else if (errorCode.ErrorCode.Code == ErrorCode.没有找到对应条目.Code)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                model.Url = filterContext.HttpContext.Request.UrlReferrer == null ? UrlHelper.GetVirtualPath("Home", "Index") :
                filterContext.HttpContext.Request.UrlReferrer.AbsolutePath;
            }
            else if (errorCode.ErrorCode.Code == ErrorCode.没有操作权限.Code)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                model.Url = filterContext.HttpContext.Request.UrlReferrer == null ? UrlHelper.GetVirtualPath("Home", "Index") :
                   filterContext.HttpContext.Request.UrlReferrer.AbsolutePath;
            }
            model.Msg = errorCode.ErrorCode.Message;
            //filterContext.HttpContext.Response.ContentType = "application/json";
            filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
            filterContext.HttpContext.Response.Write(JsonConvert.SerializeObject(model));
            filterContext.ExceptionHandled = true;
        }
    }
}
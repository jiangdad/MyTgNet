using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace PersonalDiary.Filter
{
    public static class UrlHelper
    {

        public static string GetVirtualPath(string controllerName, string actionName, string routeName = "Default")
        {
            //获取路由列表里面的指定路由（Default）的路由对象  
            VirtualPathData vp = RouteTable.Routes.GetVirtualPath(null, routeName, new RouteValueDictionary(new { controller = controllerName, action = actionName }));
            return vp.VirtualPath;
        }


        public static string GetVirtualPath(string controllerName, string actionName, int id, string routeName = "Default")
        {
            //获取路由列表里面的指定路由（Default）的路由对象  
            VirtualPathData vp = RouteTable.Routes.GetVirtualPath(null, routeName, new RouteValueDictionary(new { controller = controllerName, action = actionName, Id = id }));
            return vp.VirtualPath;
        }
    }
}
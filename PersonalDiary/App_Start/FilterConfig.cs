using PersonalDiary.Filter;
using System.Web;
using System.Web.Mvc;

namespace PersonalDiary
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthenticationAttribute());
            filters.Add(new Autho());
        }
    }
}

using System.Web;
using System.Web.Mvc;

namespace ServiceStack_AspNet_Mvc4_Example
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
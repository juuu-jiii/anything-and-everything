using System.Web;
using System.Web.Mvc;

namespace AGTIV.SPApp.TemplateWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

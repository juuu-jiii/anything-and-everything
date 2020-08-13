using System.Web;
using System.Web.Mvc;

namespace AGTIV.SPApp.TemplateWebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

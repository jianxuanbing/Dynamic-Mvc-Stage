using DynamicMvcStage.Service.Filters;
using System.Web;
using System.Web.Mvc;

namespace DynamicMvcStage.Sample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TestFilterAttribute() { Action = "All" });
        }
    }
}

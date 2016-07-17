using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicMvcStage.Service.Filters
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Interface, Inherited = true , AllowMultiple = false)]
    public class TestFilterAttribute : ActionFilterAttribute
    {
        public string Action { get; set; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write($"TestFilter\n{Action}");
            base.OnActionExecuted(filterContext);
        }
    }
}

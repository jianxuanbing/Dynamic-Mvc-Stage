using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.Controllers
{
    class DynamicControllerContextManager : IDynamicControllerContextManager
    {
        private static readonly Dictionary<string , DynamicControllerContext> contextDic = new Dictionary<string , DynamicControllerContext>();

        public void RegisterContext(string controllerName , DynamicControllerContext context)
        {
            if (string.IsNullOrEmpty(controllerName)) throw new ArgumentNullException(nameof(controllerName));
            if (context == null) throw new ArgumentNullException(nameof(context));
            contextDic.Add(controllerName.ToLower() , context);
        }

        public DynamicControllerContext GetContext(string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName)) throw new ArgumentNullException(nameof(controllerName));
            DynamicControllerContext context;
            contextDic.TryGetValue(controllerName.ToLower() , out context);
            return context;
        }

        internal static void DirectRegisterContext(string controllerName , DynamicControllerContext context)
        {
            new DynamicControllerContextManager().RegisterContext(controllerName , context);
        }
    }
}

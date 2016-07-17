using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicMvcStage.Core.MetaData;
using System.Reflection.Emit;
using System.Reflection;
using System.Web.Mvc;

namespace DynamicMvcStage.Core.Controllers.Builders
{
    public class DynamicViewResultBuilder : IDynamicActionResultBuilder
    {
        private static MethodInfo ViewMethod = typeof(Controller).GetMethod("View" , BindingFlags.NonPublic | BindingFlags.Instance , null , new Type[] { typeof(string) , typeof(string) , typeof(object) } , null);
        public object BuildActionResult(DynamicActionResultMetaData dynamicActionResultMetaData)
        {
            DynamicViewResultMetaData dynamicViewResultMetaData = dynamicActionResultMetaData as DynamicViewResultMetaData;
            MethodBuilder dynamicActionMethodBuilder = (MethodBuilder)((dynamic)dynamicActionResultMetaData.State).DynamicActionMethodBuilder;
            FieldBuilder serviceFieldBuilder = (FieldBuilder)((dynamic)dynamicActionResultMetaData.State).ServiceFieldBuilder;
            ILGenerator ilGenerator = dynamicActionMethodBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld , serviceFieldBuilder);
            ilGenerator.EmitCall(OpCodes.Callvirt , dynamicViewResultMetaData.Method , null);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldstr , dynamicViewResultMetaData.ViewName);
            ilGenerator.Emit(OpCodes.Ldstr , dynamicViewResultMetaData.MasterName ?? string.Empty);
            ilGenerator.Emit(OpCodes.Ldnull);
            ilGenerator.EmitCall(OpCodes.Call , ViewMethod , null);
            ilGenerator.Emit(OpCodes.Ret);
            return null;
        }
    }
}

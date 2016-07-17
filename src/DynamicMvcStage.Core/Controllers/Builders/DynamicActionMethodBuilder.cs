using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DynamicMvcStage.Core.MetaData;
using System.Reflection;
using System.Web.Mvc;

namespace DynamicMvcStage.Core.Controllers.Builders
{
    class DynamicActionMethodBuilder : IDynamicActionMethodBuilder
    {
        private readonly IDynamicAttributeBuilder dynamicAttributeBuilder;
        private readonly IDynamicActionResultBuilder dynamicActionResultBuilder;
        public DynamicActionMethodBuilder(IDynamicAttributeBuilder dynamicAttributeBuilder, IDynamicActionResultBuilder dynamicActionResultBuilder)
        {
            this.dynamicAttributeBuilder = dynamicAttributeBuilder;
            this.dynamicActionResultBuilder = dynamicActionResultBuilder;
        }
        public virtual MethodBuilder BuildActionMethod(DynamicActionMetaData dynamicActionMetaData)
        {
            TypeBuilder dynamicTypeBuilder = (TypeBuilder)((dynamic)dynamicActionMetaData.State).DynamicTypeBuilder;
            MethodBuilder dynamicActionMethodBuilder = dynamicTypeBuilder.DefineMethod(dynamicActionMetaData.ActionName , MethodAttributes.Public , typeof(ActionResult) , dynamicActionMetaData.Method.GetParameters().Select(p => p.ParameterType).ToArray());
            DynamicBuilderHelper.DefineDynamicAttribute(dynamicActionMethodBuilder , dynamicAttributeBuilder , dynamicActionMetaData.GetDynamicAttributes());
            ILGenerator ilGenerator = dynamicActionMethodBuilder.GetILGenerator();
            DynamicActionResultMetaData dynamicActionResultMetaData = dynamicActionMetaData.GetActionResult();
            ((dynamic)dynamicActionMetaData.State).DynamicActionMethodBuilder = dynamicActionMethodBuilder;
            dynamicActionResultMetaData.State = dynamicActionMetaData.State;
            dynamicActionResultBuilder.BuildActionResult(dynamicActionResultMetaData);
            return dynamicActionMethodBuilder;
        }
    }
}

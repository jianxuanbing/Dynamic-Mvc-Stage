using DynamicMvcStage.Core.MetaData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Reflection;

namespace DynamicMvcStage.Core.Controllers.Builders
{
    internal class DynamicControllerTypeBuilder : IDynamicControllerTypeBuilder
    {
        private static readonly ConcurrentDictionary<string , Type> controllerTypeDic = new ConcurrentDictionary<string , Type>();
        private readonly IDynamicActionMethodBuilder dynamicActionMethodBuilder;
        private readonly IDynamicAttributeBuilder dynamicAttributeBuilder;
        public DynamicControllerTypeBuilder(IDynamicAttributeBuilder dynamicAttributeBuilder, IDynamicActionMethodBuilder dynamicActionMethodBuilder)
        {
            this.dynamicAttributeBuilder = dynamicAttributeBuilder;
            this.dynamicActionMethodBuilder = dynamicActionMethodBuilder;
        }

        public Type BuildControllerType(DynamicControllerContext context)
        {
            return context.ControllerType = controllerTypeDic.GetOrAdd(context.ControllerName , key => CreateType(context.DynamicControllerMetaData));
        }

        private Type CreateType(DynamicControllerMetaData controllerMetaData)
        {
            TypeBuilder dynamicTypeBuilder = DynamicBuilderHelper.DefineControllerTypeBuilder(controllerMetaData);        
            FieldBuilder serviceFieldBuilder = DynamicBuilderHelper.DefineServiceField(dynamicTypeBuilder , controllerMetaData);
            DynamicBuilderHelper.DefineConstructor(dynamicTypeBuilder , serviceFieldBuilder , controllerMetaData.ServiceType);
            DynamicBuilderHelper.DefineActionMethod(dynamicTypeBuilder , serviceFieldBuilder, dynamicActionMethodBuilder , controllerMetaData.GetDynamicActions());
            DynamicBuilderHelper.DefineDynamicAttribute(dynamicTypeBuilder , dynamicAttributeBuilder , controllerMetaData.GetDynamicAttributes());
            var type = dynamicTypeBuilder.CreateType();
            //DynamicBuilderHelper.SaveAssembly();
            return type;
        }
    }
}

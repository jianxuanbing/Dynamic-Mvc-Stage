using DynamicMvcStage.Core.MetaData;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicMvcStage.Core.Controllers.Builders
{
    internal static class DynamicBuilderHelper
    {
        const string ASSEMBLY_NAME = "DynamicMvcStage.Generate";
        private static readonly AssemblyBuilder assemblyBuilder;
        private static readonly ModuleBuilder moduleBuilder;
        static DynamicBuilderHelper()
        {
            AssemblyName assemblyName = new AssemblyName(ASSEMBLY_NAME);
            assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName , AssemblyBuilderAccess.RunAndSave , "D:\\");
            moduleBuilder = assemblyBuilder.DefineDynamicModule(ASSEMBLY_NAME , $"{ASSEMBLY_NAME}.dll");
        }
        public static void SaveAssembly()
        {
            assemblyBuilder.Save($"{ASSEMBLY_NAME}.dll");
        }
        public static ModuleBuilder ModuleBuilder => moduleBuilder;

        private static TypeAttributes ControllerTypeAttributes = TypeAttributes.Public | TypeAttributes.AutoLayout | TypeAttributes.Class | TypeAttributes.BeforeFieldInit;
        public static TypeBuilder DefineControllerTypeBuilder(DynamicControllerMetaData controllerMetaData)
        {
            TypeBuilder dynamicTypeBuilder = moduleBuilder.DefineType($"{ASSEMBLY_NAME}.Controllers.{controllerMetaData.ControllerName}" , ControllerTypeAttributes , typeof(Controller));
            return dynamicTypeBuilder;
        }
        public static void DefineDynamicAttribute(TypeBuilder dynamicTypeBuilder , IDynamicAttributeBuilder dynamicAttributeBuilder , DynamicAttributeMetaData[] dynamicAttributeMetaDatas)
        {
            if (dynamicAttributeMetaDatas != null && dynamicAttributeMetaDatas.Any())
            {
                foreach (DynamicAttributeMetaData dynamicAttributeMetaData in dynamicAttributeMetaDatas)
                {
                    CustomAttributeBuilder attributeBuilder = dynamicAttributeBuilder.BuildCustomAttributeBuilder(dynamicAttributeMetaData);
                    dynamicTypeBuilder.SetCustomAttribute(attributeBuilder);
                }
            }
        }

        public static void DefineDynamicAttribute(MethodBuilder dynamicActionMethoduilder , IDynamicAttributeBuilder dynamicAttributeBuilder , DynamicAttributeMetaData[] dynamicAttributeMetaDatas)
        {
            if (dynamicAttributeMetaDatas != null && dynamicAttributeMetaDatas.Any())
            {
                foreach (DynamicAttributeMetaData dynamicAttributeMetaData in dynamicAttributeMetaDatas)
                {
                    CustomAttributeBuilder attributeBuilder = dynamicAttributeBuilder.BuildCustomAttributeBuilder(dynamicAttributeMetaData);
                    dynamicActionMethoduilder.SetCustomAttribute(attributeBuilder);
                }
            }
        }
        public static FieldBuilder DefineServiceField(TypeBuilder dynamicTypeBuilder , DynamicControllerMetaData controllerMetaData)
        {
            FieldBuilder serviceFieldBuilder = dynamicTypeBuilder.DefineField("_service" , controllerMetaData.ServiceType , FieldAttributes.Private | FieldAttributes.InitOnly);
            return serviceFieldBuilder;
        }

        private readonly static ConstructorInfo baseCtor = typeof(Controller).GetTypeInfo().DeclaredConstructors.First(c => !c.IsStatic);
        public static void DefineConstructor(TypeBuilder dynamicTypeBuilder , FieldBuilder serviceFieldBuilder , Type serviceType)
        {
            ConstructorBuilder constructorBuilder = dynamicTypeBuilder.DefineConstructor(MethodAttributes.Public , baseCtor.CallingConvention , new Type[] { serviceType });
            ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call , baseCtor);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Stfld , serviceFieldBuilder);
            ilGenerator.Emit(OpCodes.Ret);
        }

        public static void DefineActionMethod(TypeBuilder dynamicTypeBuilder , FieldBuilder serviceFieldBuilder , IDynamicActionMethodBuilder dynamicActionMethodBuilder , DynamicActionMetaData[] dynamicActionMetaDatas)
        {
            if (dynamicActionMetaDatas != null && dynamicActionMetaDatas.Any())
            {
                foreach (DynamicActionMetaData dynamicActionMetaData in dynamicActionMetaDatas)
                {
                    dynamic state = new ExpandoObject();
                    state.ServiceFieldBuilder = serviceFieldBuilder;
                    state.DynamicTypeBuilder = dynamicTypeBuilder;
                    dynamicActionMetaData.State = state;
                    dynamicActionMethodBuilder.BuildActionMethod(dynamicActionMetaData);
                }
            }
        }
    }
}

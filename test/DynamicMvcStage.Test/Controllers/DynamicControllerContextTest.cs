using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicMvcStage.Core;
using DynamicMvcStage.Core.Controllers;

namespace DynamicMvcStage.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DynamicControllerContext context = new DynamicControllerContext("HomeController");
            string controllerName = context.ControllerName;
            Assert.AreEqual(controllerName, "dynamic_homeController");
        }
    }
}

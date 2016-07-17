using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicMvcStage.Core.DependencyInjection;

namespace DynamicMvcStage.Test.DependencyInjection
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void ResolveInterfaceTest()
        {
            Container container = new Container();
            container.Register<IMock , Mock>();
            IMock mock = container.Resolve<IMock>();
            Assert.IsNotNull(mock);
            Assert.IsInstanceOfType(mock , typeof(IMock));
        }

        [TestMethod]
        public void ResolveClassTest()
        {
            Container container = new Container();
            container.Register<Mock , Mock>();
            IMock mock = container.Resolve<Mock>();
            Assert.IsNotNull(mock);
            Assert.IsInstanceOfType(mock , typeof(Mock));
        }

        [TestMethod]
        public void ResolveGenericClassTest()
        {
            Container container = new Container();
            container.Register<IMock<ContainerTest> , Mock<ContainerTest>>();
            IMock<ContainerTest> mock = container.Resolve<IMock<ContainerTest>>();
            Assert.IsNotNull(mock);
            Assert.IsInstanceOfType(mock , typeof(IMock<ContainerTest>));
        }

       
        private interface IMock
        {
        }

        private class Mock : IMock
        {
        }

        private interface IMock<T>
        {
        }

        private class Mock<T> : IMock<T>
        {
        }
    }
}

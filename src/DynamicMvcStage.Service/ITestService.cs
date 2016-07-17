using DynamicMvcStage.Core.MetaData.Attributes;
using DynamicMvcStage.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Service
{
    [TestFilter(Action = "DynamicController Test")]
    [Controller(name: "Test")]
    public interface ITestService
    {
        [TestFilter(Action = " \nDynamicAction Test")]
        [View(name: "Index")]
        [Action(name: "Index")]
        void Index();
    }
}

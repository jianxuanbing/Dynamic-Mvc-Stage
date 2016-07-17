using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData
{
    public class DynamicViewResultMetaData : DynamicActionResultMetaData
    {
        public string ViewName { get; }
        public string MasterName { get; }

        public DynamicViewResultMetaData(string viewName,string masterName)
        {
            ViewName = viewName;
            MasterName = masterName;
        }
    }
}

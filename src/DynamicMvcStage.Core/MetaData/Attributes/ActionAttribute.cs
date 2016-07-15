using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAttribute : Attribute
    {
        public string Name { get; set; }

        public ActionAttribute() { }

        public ActionAttribute(string name)
        {
            Name = name;
        }
    }
}

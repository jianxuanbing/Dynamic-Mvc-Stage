using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData.Attributes
{
    public class ViewAttribute : ActionResultAttribute
    {
        public string Name { get; set; }
        public string Master { get; set; }
        public ViewAttribute() { }
        public ViewAttribute(string name)
        {
            Name = name;
        }
        public ViewAttribute(string name , string master)
            :this(name)
        {
            Master = master;
        }
    }
}

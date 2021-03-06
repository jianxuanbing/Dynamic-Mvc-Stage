﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ControllerAttribute : Attribute
    {
        public string Name { get; set; }

        public ControllerAttribute() { }

        public ControllerAttribute(string name)
        {
            Name = name;
        }
    }
}

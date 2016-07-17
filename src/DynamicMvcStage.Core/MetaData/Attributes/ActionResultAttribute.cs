﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMvcStage.Core.MetaData.Attributes
{
    [AttributeUsage(AttributeTargets.Method , AllowMultiple = false , Inherited = false)]
    public abstract class ActionResultAttribute : Attribute
    {
    }
}

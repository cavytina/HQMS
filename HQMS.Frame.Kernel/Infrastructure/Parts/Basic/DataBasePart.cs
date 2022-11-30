﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public enum DataBasePart
    {
        [Description("本地数据库")]
        Native = 1,

        [Description("病案管理数据库")]
        BAGLDB = 2
    }
}

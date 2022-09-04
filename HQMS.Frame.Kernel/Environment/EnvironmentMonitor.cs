using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HQMS.Frame.Kernel.Core;

namespace HQMS.Frame.Kernel.Environment
{
    public class EnvironmentMonitor : IEnvironmentMonitor
    {
        public ValidationResults ValidationResults { get; set; }

        public PathCollecter AppPathSetting { get; set; }
        public DataBaseCollecter DataBaseSetting { get; set; }

        public EnvironmentMonitor()
        {
            ValidationResults = new ValidationResults();

            AppPathSetting = new PathCollecter();
            DataBaseSetting = new DataBaseCollecter();
        }
    }
}

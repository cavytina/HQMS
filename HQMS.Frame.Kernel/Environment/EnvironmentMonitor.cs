using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HQMS.Frame.Kernel.Infrastructure;

namespace HQMS.Frame.Kernel.Environment
{
    public class EnvironmentMonitor : IEnvironmentMonitor
    {
        public ValidationResults ValidationResults { get; set; }

        public ApplicationKind ApplicationSetting { get; set; }
        public AccountKind AccountSetting { get; set; }

        public PathCollecter PathSetting { get; set; }
        public LogCollecter LogSetting { get; set; }
        public DataBaseCollecter DataBaseSetting { get; set; }
        public MenuCollerter MenuSettings { get; set; }

        public EnvironmentMonitor()
        {
            ValidationResults = new ValidationResults();

            ApplicationSetting = new ApplicationKind();
            AccountSetting = new AccountKind();

            PathSetting = new PathCollecter();
            LogSetting = new LogCollecter();
            DataBaseSetting = new DataBaseCollecter();
            MenuSettings = new MenuCollerter();
        }
    }
}

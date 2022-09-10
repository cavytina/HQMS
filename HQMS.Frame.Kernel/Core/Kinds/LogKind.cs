using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Core
{
    public class LogKind : BaseKind
    {
        public ILogController LogController { get; set; }

        public LogKind() : base()
        {

        }

        public LogKind(string codeArgs, string nameArgs, string contentArgs, string descriptionArgs,
                                int rankArgs, bool flagArgs, ILogController logControllerArgs)
                    : base(codeArgs, nameArgs, contentArgs, descriptionArgs, rankArgs, flagArgs)
        {
            LogController = logControllerArgs;
        }
    }
}

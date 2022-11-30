using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    public class ServicEventLogController : LogControllerBase
    {
        IEnvironmentMonitor environmentMonitor;

        public override string TextLogFilePath { get; set; }

        string textLogFilePath;

        public ServicEventLogController(IContainerProvider containerProviderArgs) : base(containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();

            textLogFilePath = environmentMonitor.PathSetting.GetContent("TextLogFilePath");

            TextLogFilePath = textLogFilePath.Insert(textLogFilePath.IndexOf(".txt"), "_ServicEvent");
        }
    }
}

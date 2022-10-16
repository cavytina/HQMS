using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Infrastructure;

namespace HQMS.Frame.Kernel.Services
{
    public interface IEventServiceController
    {
        string Request(EventServicePart eventServiceArgs, FrameModulePart sourceModuleArgs, FrameModulePart targetModuleArgs, IServiceContent serviceContentArgs);

        string Response(EventServicePart eventServiceArgs, FrameModulePart sourceModuleArgs, FrameModulePart targetModuleArgs, bool returnCodeArgs, string returnMessageArgs, IServiceContent serviceContentArgs);
    }
}

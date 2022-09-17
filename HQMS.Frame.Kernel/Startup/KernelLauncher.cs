using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Events;
using HQMS.Frame.Kernel.Events;

namespace HQMS.Frame.Kernel
{
    public class KernelLauncher
    {
        IEventAggregator eventAggregator;

        public KernelLauncher(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
        }
    }
}

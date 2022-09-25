using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Infrastructure;

namespace HQMS.Frame.Kernel.Services
{
    public interface IJsonTextController
    {
        string ConvertToText(ServiceEventPart serviceEventPartArgs,IServiceContent serviceContentArgs);
    }
}

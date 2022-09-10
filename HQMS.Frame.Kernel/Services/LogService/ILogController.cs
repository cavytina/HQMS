using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    public interface ILogController
    {
        void WriteLog(string messageArgs);
    }
}

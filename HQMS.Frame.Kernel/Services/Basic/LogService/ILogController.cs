using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Services
{
    public interface ILogController
    {
        string TextLogFilePath { get; set; }

        void Initialize();
        void WriteLog(string messageArgs);
    }
}

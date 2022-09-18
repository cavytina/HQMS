using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Infrastructure;

namespace HQMS.Frame.Service.Peripherals
{
    public interface IAccountAuthenticationControler
    {
        bool Validate(RequestAccountKind requestAccountKindArgs);
    }
}

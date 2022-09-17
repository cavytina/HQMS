using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Service.Peripherals
{
    public interface IOneWayCipherController
    {
        string Encrypt(string plaintextArgs);
    }
}

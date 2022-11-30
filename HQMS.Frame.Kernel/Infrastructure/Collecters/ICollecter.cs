using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public interface ICollecter<T>
    {
        T GetContent(string nameIndex);
    }
}

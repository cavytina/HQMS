using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class MenuCollerter : List<MenuKind>
    {
        public string GetContent(string codeIndexArgs)
        {
            return Find(x => x.Code == codeIndexArgs).ReferName;
        }

        public MenuKind this[string codeIndex]
        {
            set { base[FindIndex(x => x.Code == codeIndex)] = value; }
            get { return Find(x => x.Code == codeIndex); }
        }
    }
}

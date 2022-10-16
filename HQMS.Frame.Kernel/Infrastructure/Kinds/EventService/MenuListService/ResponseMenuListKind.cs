using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class ResponseMenuListKind : IServiceContent
    {
        [JsonProperty(PropertyName = "menus")]
        public List<MenuKind> MenuList { get; set; }

        public ResponseMenuListKind()
        {
            MenuList = new List<MenuKind>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class RequestMenuItemKind: IServiceContent
    {
        [JsonProperty(PropertyName = "menu")]
        public string MenuItemCode { get; set; }
    }
}

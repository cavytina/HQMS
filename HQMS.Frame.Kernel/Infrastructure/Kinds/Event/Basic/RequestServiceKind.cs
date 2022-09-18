using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class RequestServiceKind : BaseKind
    {
        [JsonProperty(PropertyName = "request_module_name")]
        public string RequestModuleName { get; set; }

        [JsonProperty(PropertyName = "serv_cont")]
        public IServiceContent ServiceContent { get; set; }
    }
}

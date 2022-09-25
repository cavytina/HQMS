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
        [JsonProperty(PropertyName = "req_mod_name")]
        public string RequestModuleName { get; set; }

        [JsonProperty(PropertyName = "svc_cry")]
        public IServiceContent ServiceContent { get; set; }
    }
}

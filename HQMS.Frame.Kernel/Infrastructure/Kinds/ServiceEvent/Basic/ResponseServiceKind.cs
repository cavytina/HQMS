using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class ResponseServiceKind : BaseKind
    {
        [JsonProperty(PropertyName = "resp_mod_name")]
        public string ResponseModuleName { get; set; }

        [JsonProperty(PropertyName = "ret_code")]
        public string ReturnCode { get; set; }

        [JsonProperty(PropertyName = "ret_msg")]
        public string ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "svc_cry")]
        public IServiceContent ServiceContent { get; set; }
    }
}

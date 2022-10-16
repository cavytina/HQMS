using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class ResponseServiceKind : BaseKind
    {
        [JsonProperty(PropertyName = "souc_mod_name")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FrameModulePart SourceModuleName { get; set; }

        [JsonProperty(PropertyName = "tagt_mod_name")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FrameModulePart TargetModuleName { get; set; }

        [JsonProperty(PropertyName = "ret_code")]
        public string ReturnCode { get; set; }

        [JsonProperty(PropertyName = "ret_msg")]
        public string ReturnMessage { get; set; }

        [JsonProperty(PropertyName = "svc_cont")]
        public IServiceContent ServiceContent { get; set; }
    }
}

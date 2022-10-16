using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class RequestServiceKind : BaseKind
    {
        [JsonProperty(PropertyName = "souc_mod_name")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FrameModulePart SourceModuleName { get; set; }

        [JsonProperty(PropertyName = "tagt_mod_name")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FrameModulePart TargetModuleName { get; set; }

        [JsonProperty(PropertyName = "svc_cont")]
        public IServiceContent ServiceContent { get; set; }
    }
}

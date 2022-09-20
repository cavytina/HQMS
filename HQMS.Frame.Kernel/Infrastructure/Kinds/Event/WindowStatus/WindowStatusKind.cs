using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class WindowStatusKind :IServiceContent
    {
        [JsonProperty(PropertyName = "Win_Sts")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WindowStatusPart WindowStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class ApplicationStatusKind : IServiceContent
    {
        [JsonProperty(PropertyName = "App_Stas")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ApplicationStatusPart ApplicationStatus { get; set; }
    }
}

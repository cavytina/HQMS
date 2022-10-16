using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class ResponseAccountKind : IServiceContent
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}

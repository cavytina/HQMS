using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class NestKind : BaseKind
    {
        [JsonProperty(PropertyName = "super_code")]
        public string SuperCode { get; set; }

        [JsonProperty(PropertyName = "super_name")]
        public string SuperName { get; set; }

        public NestKind() : base()
        {

        }

        public NestKind(string codeArgs, string nameArgs, string contentArgs, string descriptionArgs,
                        string superCodeArgs, string superNameArgs, int rankArgs, bool flagArgs) : base(codeArgs, nameArgs, contentArgs, descriptionArgs, rankArgs, flagArgs)
        {
            SuperCode = superCodeArgs;
            SuperName = superCodeArgs;
        }
    }
}

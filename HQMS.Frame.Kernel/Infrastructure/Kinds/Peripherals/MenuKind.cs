using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class MenuKind : NestKind
    {
        [JsonProperty(PropertyName = "refer_name")]
        public string ReferName { get; set; }

        public MenuKind() : base()
        {

        }

        public MenuKind(string codeArgs, string nameArgs, string referNameArgs, string contentArgs, string descriptionArgs,
                string superCodeArgs, string superNameArgs, int rankArgs, bool flagArgs) : base(codeArgs, nameArgs, contentArgs, descriptionArgs, superCodeArgs, superNameArgs, rankArgs, flagArgs)
        {
            ReferName = referNameArgs;
        }
    }
}

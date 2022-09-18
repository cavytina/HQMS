using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HQMS.Frame.Kernel.Infrastructure
{
    /// <summary>
    /// 基础字典设置类型
    /// </summary>
    public class BaseKind
    {
        [JsonProperty(PropertyName = "svc_code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "svc_name")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "svc_desc")]
        public string Description { get; set; }

        [JsonIgnore]
        public int Rank { get; set; }

        [JsonIgnore]
        public bool Flag { get; set; }

        public BaseKind()
        {

        }

        public BaseKind(string codeArgs, string nameArgs, string contentArgs, string descriptionArgs, int rankArgs, bool flagArgs)
        {
            Code = codeArgs;
            Name = nameArgs;
            Content = contentArgs;
            Description = descriptionArgs;
            Rank = rankArgs;
            Flag = flagArgs;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Core
{
    /// <summary>
    /// 基础字典设置类型
    /// </summary>
    public class BaseKind
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
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

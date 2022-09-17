using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class CategoryKind : BaseKind
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }

        public CategoryKind() : base()
        {

        }

        public CategoryKind(string codeArgs, string nameArgs, string contentArgs, string descriptionArgs,
                                string categoryCodeArgs, string categoryNameArgs, int rankArgs, bool flagArgs) : base(codeArgs, nameArgs, contentArgs, descriptionArgs, rankArgs, flagArgs)
        {
            CategoryCode = categoryCodeArgs;
            CategoryName = categoryNameArgs;
        }
    }
}

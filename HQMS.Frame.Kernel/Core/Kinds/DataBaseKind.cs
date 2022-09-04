using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Core
{
    public class DataBaseKind : BaseKind
    {
        public IDataBaseController DataBaseController { get; set; }

        public DataBaseKind() : base()
        {

        }

        public DataBaseKind(string codeArgs, string nameArgs, string contentArgs, string descriptionArgs,
                                string categoryCodeArgs, string categoryNameArgs, int rankArgs, bool flagArgs,IDataBaseController dataBaseControllerArgs)
                    : base(codeArgs, nameArgs, contentArgs, descriptionArgs, rankArgs, flagArgs)
        {
            DataBaseController = dataBaseControllerArgs;
        }
    }
}

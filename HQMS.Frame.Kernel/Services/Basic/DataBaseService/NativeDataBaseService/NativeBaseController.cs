using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Prism.Ioc;
using System.Data.SQLite;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    [HasSelfValidation]
    public class NativeBaseController : DataBaseControllerBase
    {
        string sqliteConnectionString;
        IEnvironmentMonitor environmentMonitor;

        public NativeBaseController(IContainerProvider containerProviderArgs) : base(containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();

            sqliteConnectionString = environmentMonitor.DataBaseSetting[0].Content;
            base.DBConnection = new SQLiteConnection(sqliteConnectionString);
        }

        [SelfValidation]
        public override void Validate(ValidationResults results)
        {
            base.DBConnection.Open();
            if (base.DBConnection.State != ConnectionState.Open)
                results.AddResult(new ValidationResult("当前本地数据库文件访问失败!", this, "sqliteConnection", "", null));
            base.DBConnection.Close();
            base.DBConnection.Dispose();
        }
    }
}

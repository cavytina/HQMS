using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Prism.Ioc;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    public class BAGLDBController : DataBaseControllerBase
    {
        string baglDBConnectionString;
        IEnvironmentMonitor environmentMonitor;

        public BAGLDBController(IContainerProvider containerProviderArgs) : base(containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();

            baglDBConnectionString = environmentMonitor.DataBaseSetting["BAGLDB"].Content;
            base.DBConnection = new SqlConnection(baglDBConnectionString);
        }
    }
}

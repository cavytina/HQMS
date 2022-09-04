using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Prism.Ioc;
using Dapper;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    [HasSelfValidation]
    public abstract class DataBaseControllerBase : IDataBaseController
    {
        ILogController logController;
        IEnvironmentMonitor environmentMonitor;

        IDbConnection dbConnection;
        protected IDbConnection DBConnection
        {
            get => dbConnection;
            set => dbConnection = value;
        }

        public DataBaseControllerBase(IContainerProvider containerProviderArgs)
        {
            logController = containerProviderArgs.Resolve<ILogController>();
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
        }

        [SelfValidation]
        public virtual void Validate(ValidationResults results)
        {

        }

        public virtual bool Query<T>(string queryStingArgs, out List<T> tHub)
        {
            bool ret = false;
            tHub = default(List<T>);

            if (environmentMonitor.ValidationResults.IsValid)
            {
                dbConnection.Open();
                CommandDefinition commandDefinition = new CommandDefinition(queryStingArgs);
                tHub = SqlMapper.Query<T>(dbConnection, commandDefinition).AsList();
                logController.WriteLog(queryStingArgs);
                ret = true;
                dbConnection.Close();
            }

            return ret;
        }

        public virtual bool Execute(string execStingArgs)
        {
            bool ret = false;

            if (environmentMonitor.ValidationResults.IsValid)
            {
                dbConnection.Open();
                CommandDefinition commandDefinition = new CommandDefinition(execStingArgs);
                int retVal = SqlMapper.Execute(dbConnection, commandDefinition);
                logController.WriteLog(execStingArgs);
                ret = true;
                dbConnection.Close();
            }

            return ret;
        }

        public virtual bool ExecuteScalar(string execStingArgs, out object objArgs)
        {
            bool ret = false;
            objArgs = default(object);

            if (environmentMonitor.ValidationResults.IsValid)
            {
                dbConnection.Open();
                CommandDefinition commandDefinition = new CommandDefinition(execStingArgs);
                objArgs = SqlMapper.ExecuteScalar(dbConnection, commandDefinition);
                logController.WriteLog(execStingArgs);
                ret = true;
                dbConnection.Close();
            }

            return ret;
        }
    }
}

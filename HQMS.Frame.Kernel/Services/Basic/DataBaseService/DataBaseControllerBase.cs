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
using HQMS.Frame.Kernel.Infrastructure;
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

        IDataReader reader;

        public DataBaseControllerBase(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            logController = environmentMonitor.LogSetting.GetContent("DataBase");
        }

        [SelfValidation]
        public virtual void Validate(ValidationResults results)
        {

        }

        public virtual bool Query<T>(string queryStringArg, out List<T> tHub)
        {
            bool ret = false;
            tHub = default(List<T>);

            if (environmentMonitor.ValidationResults.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(queryStringArg);

                try
                {
                    dbConnection.Open();
                    tHub = SqlMapper.Query<T>(dbConnection, commandDefinition).AsList();
                }
                catch (Exception ex)
                {
                    logController.WriteLog(ex.Message);
                }
                finally
                {
                    ret = true;
                    logController.WriteLog(queryStringArg);
                    dbConnection.Close();
                }
            }

            return ret;
        }

        public virtual bool QueryWithMessage<T>(string queryStringArg, out List<T> tHub, out string retStringArg)
        {
            bool ret = false;
            tHub = default(List<T>);
            retStringArg = string.Empty;

            if (environmentMonitor.ValidationResults.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(queryStringArg);

                try
                {
                    dbConnection.Open();
                    reader = SqlMapper.ExecuteReader(dbConnection, commandDefinition);
                    reader.Read();
                    if (reader.GetString(0) == "F")
                        retStringArg = reader.GetString(1);
                    else
                    {
                        if (!reader.IsClosed)
                        {
                            reader.Close();
                            tHub = SqlMapper.Query<T>(dbConnection, commandDefinition).AsList();
                            ret = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logController.WriteLog(ex.Message);
                }
                finally
                {
                    logController.WriteLog(queryStringArg);
                    dbConnection.Close();
                }
            }

            return ret;
        }

        public virtual bool Execute(string execStingArgs)
        {
            bool ret = false;

            if (environmentMonitor.ValidationResults.IsValid)
            {
                CommandDefinition commandDefinition;
                int retVal = 0;

                try
                {
                    dbConnection.Open();
                    commandDefinition = new CommandDefinition(execStingArgs);
                    retVal = SqlMapper.Execute(dbConnection, commandDefinition);
                }
                catch (Exception ex)
                {
                    logController.WriteLog(ex.Message);
                }
                finally
                {
                    ret = true;

                    if (retVal == 0)
                        logController.WriteLog("未影响: " + execStingArgs);
                    else
                        logController.WriteLog(execStingArgs);

                    dbConnection.Close();
                }
            }

            return ret;
        }

        public virtual bool ExecuteWithMessage(string execStingArgs, out string retStringArg)
        {
            bool ret = false;
            retStringArg = string.Empty;

            if (environmentMonitor.ValidationResults.IsValid)
            {
                CommandDefinition commandDefinition = new CommandDefinition(execStingArgs);

                try
                {
                    dbConnection.Open();
                    reader = SqlMapper.ExecuteReader(dbConnection, commandDefinition);
                    reader.Read();
                    retStringArg = reader.GetString(1);
                    if (reader.GetString(0) == "T")
                        ret = true;
                }
                catch (Exception ex)
                {
                    logController.WriteLog(ex.Message);
                }
                finally
                {
                    logController.WriteLog(execStingArgs);
                    dbConnection.Close();
                }
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

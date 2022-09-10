using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Core;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    [HasSelfValidation]
    public class LogManager : ILogManager
    {
        /// <summary>
        /// 默认日志文件路径
        /// </summary>
        string defaultTextLogFilePath;

        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        ILogController textLogController;
        IDataBaseController nativeController;

        string sqlString;
        object retArg;

        public LogManager(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize()
        {
            defaultTextLogFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "txt");

            if (!environmentMonitor.LogSetting.Contains("TextLog"))
                environmentMonitor.LogSetting.Add(new LogKind
                {
                    Code = "01",
                    Name = "TextLog",
                    Content = defaultTextLogFilePath,
                    Description = "文本日志",
                    Rank = 1,
                    Flag = true
                });
            else
                environmentMonitor.LogSetting["TextLog"].Content = defaultTextLogFilePath;
        }

        [SelfValidation]
        public void Validate(ValidationResults results)
        {
            if (string.IsNullOrEmpty(defaultTextLogFilePath))
                results.AddResult(new ValidationResult("默认日志文件不能为空!", this, "defaultTextLogFilePath", "", null));
        }

        public void Load()
        {
            textLogController = containerProvider.Resolve<ILogController>();
            environmentMonitor.LogSetting["TextLog"].LogController = textLogController;
        }

        public void Save()
        {
            nativeController = environmentMonitor.DataBaseSetting.GetDataBaseController("Native");

            if (nativeController != null)
            {
                foreach (LogKind logKind in environmentMonitor.LogSetting)
                {
                    sqlString = "SELECT 1 FROM System_LogSetting WHERE Name='" + logKind.Name + "'";
                    nativeController.ExecuteScalar(sqlString, out retArg);

                    if (retArg != null)
                        sqlString = "UPDATE System_PathSetting SET Content='" + logKind.Content + "' WHERE Name='" + logKind.Name + "'";
                    else
                        sqlString = "INSERT INTO System_LogSetting (Code,Name,Content,Description,Rank,Flag)" +
                        " VALUES ('" + logKind.Code + "','" + logKind.Name + "','" + logKind.Content +
                        "','" + logKind.Description + "'," + logKind.Rank + "," + logKind.Flag + ")";
                    nativeController.Execute(sqlString);
                }
            }
        }
    }
}

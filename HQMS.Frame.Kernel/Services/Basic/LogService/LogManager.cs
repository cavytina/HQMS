using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Extension;

namespace HQMS.Frame.Kernel.Services
{
    [HasSelfValidation]
    public class LogManager : ILogManager
    {
        IEnvironmentMonitor environmentMonitor;
        ILogController dataBaseLogController;
        ILogController servicEventLogController;
        IDataBaseController nativeController;

        string sqlSentence;
        object retArg;

        public LogManager(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            dataBaseLogController = containerProviderArgs.Resolve<ILogController>(LogPart.DataBase.ToString());
            servicEventLogController = containerProviderArgs.Resolve<ILogController>(LogPart.ServicEvent.ToString());
        }

        public void Initialize()
        {
            dataBaseLogController.Initialize();
            servicEventLogController.Initialize();

            if (!environmentMonitor.LogSetting.Exists(x => x.Name == LogPart.DataBase.ToString()))
                environmentMonitor.LogSetting.Add(new LogKind
                {
                    Code = Convert.ToInt32(LogPart.DataBase).ToString().PadLeft(2, '0'),
                    Name = LogPart.DataBase.ToString(),
                    Content = dataBaseLogController.TextLogFilePath,
                    Description = EnmuExtension.GetDescription(LogPart.DataBase),
                    Rank = Convert.ToInt32(LogPart.DataBase),
                    Flag = true,
                    LogController = dataBaseLogController
                });
            else
            {
                environmentMonitor.LogSetting[LogPart.DataBase.ToString()].Content = dataBaseLogController.TextLogFilePath;
                environmentMonitor.LogSetting[LogPart.DataBase.ToString()].LogController = dataBaseLogController;
            }

            if (!environmentMonitor.LogSetting.Exists(x => x.Name == LogPart.ServicEvent.ToString()))
                environmentMonitor.LogSetting.Add(new LogKind
                {
                    Code = Convert.ToInt32(LogPart.ServicEvent).ToString().PadLeft(2, '0'),
                    Name = LogPart.ServicEvent.ToString(),
                    Content = servicEventLogController.TextLogFilePath,
                    Description = EnmuExtension.GetDescription(LogPart.ServicEvent),
                    Rank = Convert.ToInt32(LogPart.ServicEvent),
                    Flag = true,
                    LogController = servicEventLogController
                });
            else
            {
                environmentMonitor.LogSetting[LogPart.ServicEvent.ToString()].Content = servicEventLogController.TextLogFilePath;
                environmentMonitor.LogSetting[LogPart.ServicEvent.ToString()].LogController = servicEventLogController;
            }
        }

        [SelfValidation]
        public void Validate(ValidationResults results)
        {
            if (string.IsNullOrEmpty(dataBaseLogController.TextLogFilePath))
                results.AddResult(new ValidationResult("默认日志文件不能为空!", this, "defaultTextLogFilePath", "", null));
        }

        public void Save()
        {
            nativeController = environmentMonitor.DataBaseSetting.GetContent("Native");

            if (nativeController != null)
            {
                foreach (LogKind logKind in environmentMonitor.LogSetting)
                {
                    sqlSentence = "SELECT 1 FROM System_LogSetting WHERE Name='" + logKind.Name + "'";
                    nativeController.ExecuteScalar(sqlSentence, out retArg);

                    if (retArg != null)
                        sqlSentence = "UPDATE System_PathSetting SET Content='" + logKind.Content + "' WHERE Name='" + logKind.Name + "'";
                    else
                        sqlSentence = "INSERT INTO System_LogSetting (Code,Name,Content,Description,Rank,Flag)" +
                        " VALUES ('" + logKind.Code + "','" + logKind.Name + "','" + logKind.Content +
                        "','" + logKind.Description + "'," + logKind.Rank + "," + logKind.Flag + ")";
                    nativeController.Execute(sqlSentence);
                }
            }
        }
    }
}

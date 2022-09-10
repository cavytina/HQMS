using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Prism.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Core;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    [HasSelfValidation]
    public class PathManager : IPathManager
    {
        /// <summary>
        /// 默认程序运行目录
        /// </summary>
        string defaultApplictionCatalogue;

        /// <summary>
        /// 默认本地数据库文件路径
        /// </summary>
        string defaultNativeDataBaseFilePath;

        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeController;

        string sqlString;
        object retArg;

        public PathManager(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize()
        {
            defaultApplictionCatalogue = AppDomain.CurrentDomain.BaseDirectory;
            defaultNativeDataBaseFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "db");

            if (!environmentMonitor.PathSetting.Contains("ApplictionCatalogue"))
                environmentMonitor.PathSetting.Add(new BaseKind
                {
                    Code = "01",
                    Name = "ApplictionCatalogue",
                    Content = defaultApplictionCatalogue,
                    Description = "程序运行目录",
                    Rank = 1,
                    Flag = true
                });
            else
                environmentMonitor.PathSetting["ApplictionCatalogue"].Content = defaultApplictionCatalogue;

            if (!environmentMonitor.PathSetting.Contains("NativeDataBaseFilePath"))
                environmentMonitor.PathSetting.Add(new BaseKind
                {
                    Code = "02",
                    Name = "NativeDataBaseFilePath",
                    Content = defaultNativeDataBaseFilePath,
                    Description = "本地数据库文件路径",
                    Rank = 2,
                    Flag = true
                });
            else
                environmentMonitor.PathSetting["NativeDataBaseFilePath"].Content = defaultNativeDataBaseFilePath;
        }

        [SelfValidation]
        public void Validate(ValidationResults results)
        {
            if (!File.Exists(defaultNativeDataBaseFilePath))
                results.AddResult(new ValidationResult("当前程序环境缺少本地数据库文件,请检查配置并重启程序!", this, "DefaultNativeDataBaseFilePath", "", null));
        }

        public void Save()
        {
            nativeController = environmentMonitor.DataBaseSetting.GetDataBaseController("Native");

            if (nativeController != null)
            {
                foreach (BaseKind pathSetting in environmentMonitor.PathSetting)
                {
                    sqlString = "SELECT 1 FROM System_PathSetting WHERE Name='" + pathSetting.Name + "'";
                    nativeController.ExecuteScalar(sqlString, out retArg);

                    if (retArg != null)
                        sqlString = "UPDATE System_PathSetting SET Content='" + pathSetting.Content + "' WHERE Name='" + pathSetting.Name + "'";
                    else
                        sqlString = "INSERT INTO System_PathSetting (Code,Name,Content,Description,Rank,Flag)" +
                        " VALUES ('" + pathSetting.Code + "','" + pathSetting.Name + "','" + pathSetting.Content +
                        "','" + pathSetting.Description + "'," + pathSetting.Rank + "," + pathSetting.Flag + ")";

                    nativeController.Execute(sqlString);
                }
            }
        }
    }
}

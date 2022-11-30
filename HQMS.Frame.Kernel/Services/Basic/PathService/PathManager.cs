using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Prism.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Extension;

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

        /// <summary>
        /// 默认日志文件路径
        /// </summary>
        string defaultTextLogFilePath;

        IEnvironmentMonitor environmentMonitor;
        IDataBaseController nativeController;

        string sqlSentence;
        object retArg;

        public PathManager(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize()
        {
            defaultApplictionCatalogue = AppDomain.CurrentDomain.BaseDirectory;
            defaultNativeDataBaseFilePath = defaultApplictionCatalogue + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "db");
            defaultTextLogFilePath = defaultApplictionCatalogue + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "txt");

            if (!environmentMonitor.PathSetting.Exists(x => x.Name == PathPart.ApplictionCatalogue.ToString()))
                environmentMonitor.PathSetting.Add(new BaseKind
                {
                    Code = Convert.ToInt32(PathPart.ApplictionCatalogue).ToString().PadLeft(2, '0'),
                    Name = PathPart.ApplictionCatalogue.ToString(),
                    Content = defaultApplictionCatalogue,
                    Description = EnmuExtension.GetDescription(PathPart.ApplictionCatalogue),
                    Rank = Convert.ToInt32(PathPart.ApplictionCatalogue),
                    Flag = true
                });
            else
                environmentMonitor.PathSetting[PathPart.ApplictionCatalogue.ToString()].Content = defaultApplictionCatalogue;

            if (!environmentMonitor.PathSetting.Exists(x => x.Name == PathPart.NativeDataBaseFilePath.ToString()))
                environmentMonitor.PathSetting.Add(new BaseKind
                {
                    Code = Convert.ToInt32(PathPart.NativeDataBaseFilePath).ToString().PadLeft(2, '0'),
                    Name = PathPart.NativeDataBaseFilePath.ToString(),
                    Content = defaultNativeDataBaseFilePath,
                    Description = EnmuExtension.GetDescription(PathPart.NativeDataBaseFilePath),
                    Rank = Convert.ToInt32(PathPart.NativeDataBaseFilePath),
                    Flag = true
                });
            else
                environmentMonitor.PathSetting[PathPart.NativeDataBaseFilePath.ToString()].Content = defaultNativeDataBaseFilePath;

            if (!environmentMonitor.PathSetting.Exists(x => x.Name == PathPart.TextLogFilePath.ToString()))
                environmentMonitor.PathSetting.Add(new BaseKind
                {
                    Code = Convert.ToInt32(PathPart.TextLogFilePath).ToString().PadLeft(2, '0'),
                    Name = PathPart.TextLogFilePath.ToString(),
                    Content = defaultTextLogFilePath,
                    Description = EnmuExtension.GetDescription(PathPart.NativeDataBaseFilePath),
                    Rank = Convert.ToInt32(PathPart.TextLogFilePath),
                    Flag = true
                });
            else
                environmentMonitor.PathSetting[PathPart.NativeDataBaseFilePath.ToString()].Content = defaultNativeDataBaseFilePath;
        }

        [SelfValidation]
        public void Validate(ValidationResults results)
        {
            if (!File.Exists(defaultNativeDataBaseFilePath))
                results.AddResult(new ValidationResult("当前程序环境缺少本地数据库文件,请检查配置并重启程序!", this, "DefaultNativeDataBaseFilePath", "", null));
        }

        public void Save()
        {
            nativeController = environmentMonitor.DataBaseSetting.GetContent("Native");

            if (nativeController != null)
            {
                foreach (BaseKind pathSetting in environmentMonitor.PathSetting)
                {
                    sqlSentence = "SELECT 1 FROM System_PathSetting WHERE Name='" + pathSetting.Name + "'";
                    nativeController.ExecuteScalar(sqlSentence, out retArg);

                    if (retArg != null)
                        sqlSentence = "UPDATE System_PathSetting SET Content='" + pathSetting.Content + "' WHERE Name='" + pathSetting.Name + "'";
                    else
                        sqlSentence = "INSERT INTO System_PathSetting (Code,Name,Content,Description,Rank,Flag)" +
                        " VALUES ('" + pathSetting.Code + "','" + pathSetting.Name + "','" + pathSetting.Content +
                        "','" + pathSetting.Description + "'," + pathSetting.Rank + "," + pathSetting.Flag + ")";

                    nativeController.Execute(sqlSentence);
                }
            }
        }
    }
}

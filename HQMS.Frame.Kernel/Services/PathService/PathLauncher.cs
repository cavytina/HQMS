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
    public class PathLauncher : IPathLauncher
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

        public PathLauncher(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize()
        {
            defaultApplictionCatalogue = AppDomain.CurrentDomain.BaseDirectory;
            defaultNativeDataBaseFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "db");
            defaultTextLogFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName.Replace("exe", "txt");
        }

        [SelfValidation]
        public void Validate(ValidationResults results)
        {
            if (!File.Exists(defaultNativeDataBaseFilePath))
                results.AddResult(new ValidationResult("当前程序环境缺少本地数据库文件,请检查配置并重启程序!", this, "DefaultNativeDataBaseFilePath", "", null));
        }

        public void Load()
        {
            if (!environmentMonitor.AppPathSetting.Contains("ApplictionCatalogue"))
                environmentMonitor.AppPathSetting.Add(new BaseKind
                {
                    Code = "01",
                    Name = "ApplictionCatalogue",
                    Content = defaultApplictionCatalogue,
                    Description = "程序运行目录",
                    Rank = 1,
                    Flag = true
                });

            if (!environmentMonitor.AppPathSetting.Contains("NativeDataBaseFilePath"))
                environmentMonitor.AppPathSetting.Add(new BaseKind
                {
                    Code = "02",
                    Name = "NativeDataBaseFilePath",
                    Content = defaultNativeDataBaseFilePath,
                    Description = "本地数据库文件路径",
                    Rank = 2,
                    Flag = true
                });

            if (!environmentMonitor.AppPathSetting.Contains("TextLogFilePath"))
                environmentMonitor.AppPathSetting.Add(new BaseKind
                {
                    Code = "03",
                    Name = "TextLogFilePath",
                    Content = defaultTextLogFilePath,
                    Description = "日志文件路径",
                    Rank = 3,
                    Flag = true
                });
        }

        public void Save()
        {

        }
    }
}

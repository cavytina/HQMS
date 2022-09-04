using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HQMS.Frame.Kernel.Core;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    public class DataBaseLauncher : IDataBaseLauncher
    {
        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        ICipherController cipherController;
        IDataBaseController nativeBaseController;
        IDataBaseController baglDBController;

        string sqliteConnectionString, sqliteCipherConnectionString;
        string baglDBConnectionString, baglDBCipherConnectionString;

        string sqlString;
        List<BaseKind> dataBaseHub;

        public DataBaseLauncher(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            cipherController = containerProviderArgs.Resolve<ICipherController>();
        }

        public void Initialize()
        {
            InnerInitialize();
            InnerLoad();

            sqlString = "SELECT Code,Name,Content,Description,Rank,Flag FROM System_DataBaseSetting";
            nativeBaseController.Query<BaseKind>(sqlString, out dataBaseHub);

            var baglDBInfo = from baglDBHub in dataBaseHub
                             where baglDBHub.Flag && baglDBHub.Name== "BAGLDB"
                             select baglDBHub;

            baglDBConnectionString = cipherController.Decrypt(baglDBInfo.FirstOrDefault().Content);
        }

        private void InnerInitialize()
        {
            sqliteConnectionString = "Data Source= " + environmentMonitor.AppPathSetting["NativeDataBaseFilePath"];

            if (!environmentMonitor.DataBaseSetting.Contains("Native"))
                environmentMonitor.DataBaseSetting.Add(new DataBaseKind
                {
                    Code = "01",
                    Name = "Native",
                    Content = sqliteConnectionString,
                    Description = "本地数据库",
                    Rank = 1,
                    Flag = true
                });
        }

        private void InnerLoad()
        {
            nativeBaseController = containerProvider.Resolve<IDataBaseController>("Native");

            environmentMonitor.DataBaseSetting["Native"] = nativeBaseController;
        }

        public void Load()
        {
            if (!environmentMonitor.DataBaseSetting.Contains("BAGLDB"))
                environmentMonitor.DataBaseSetting.Add(new DataBaseKind
                {
                    Code = "02",
                    Name = "BAGLDB",
                    Content = baglDBConnectionString,
                    Description = "病案管理数据库",
                    Rank = 1,
                    Flag = true
                });

            baglDBController= containerProvider.Resolve<IDataBaseController>("BAGLDB");
            environmentMonitor.DataBaseSetting["BAGLDB"] = baglDBController;
        }

        public void Save()
        {
            sqliteCipherConnectionString = cipherController.Encrypt(sqliteConnectionString);
            baglDBCipherConnectionString = cipherController.Encrypt(baglDBConnectionString);

            sqlString = "UPDATE System_DataBaseSetting SET Content='"+ sqliteCipherConnectionString+ "' WHERE Name='Native'";
            nativeBaseController.Execute(sqlString);

            sqlString = "UPDATE System_DataBaseSetting SET Content='" + baglDBCipherConnectionString + "' WHERE Name='BAGLDB'";
            nativeBaseController.Execute(sqlString);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Environment;

namespace HQMS.Frame.Kernel.Services
{
    public class DataBaseManager : IDataBaseManager
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

        public DataBaseManager(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            cipherController = containerProviderArgs.Resolve<ICipherController>();
        }

        public void Initialize()
        {
            InnerLoad();

            sqlString = "SELECT Code,Name,Content,Description,Rank,Flag FROM System_DataBaseSetting";
            nativeBaseController.Query<BaseKind>(sqlString, out dataBaseHub);

            var baglDBInfo = from baglDBHub in dataBaseHub
                             where baglDBHub.Flag && baglDBHub.Name== "BAGLDB"
                             select baglDBHub;

            baglDBConnectionString = cipherController.Decrypt(baglDBInfo.FirstOrDefault().Content);
        }

        private void InnerLoad()
        {
            sqliteConnectionString = "Data Source= " + environmentMonitor.PathSetting["NativeDataBaseFilePath"].Content;

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
            else
                environmentMonitor.DataBaseSetting["Native"].Content = sqliteConnectionString;

            nativeBaseController = containerProvider.Resolve<IDataBaseController>("Native");

            environmentMonitor.DataBaseSetting["Native"].DataBaseController = nativeBaseController;
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
            environmentMonitor.DataBaseSetting["BAGLDB"].DataBaseController = baglDBController;
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

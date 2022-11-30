using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Prism.Ioc;
using Prism.Mvvm;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;
using CsvHelper;

namespace HQMS.Control.Extension.PerformanceAssessment.Models
{
    public class DataQueryingModel : BindableBase
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController bagldbController;

        string sqlSentence;
        List<ResultKind> resultHub;

        DateTime beginDate = DateTime.Now.Date;
        public DateTime BeginDate
        {
            get => beginDate;
            set => SetProperty(ref beginDate, value);
        }

        DateTime endDate = DateTime.Now.Date;
        public DateTime EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }

        int currentPage;
        public int CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        int pageRecordCount = 20;
        public int PageRecordCount
        {
            get => pageRecordCount;
            set => pageRecordCount = value;
        }

        int totalRecordCount;
        public int TotalRecordCount
        {
            get => totalRecordCount;
            set => SetProperty(ref totalRecordCount, value);
        }

        ObservableCollection<ResultKind> results;
        public ObservableCollection<ResultKind> Results
        {
            get => results;
            set => SetProperty(ref results, value);
        }

        public DataQueryingModel(IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            bagldbController = environmentMonitor.DataBaseSetting.GetContent("BAGLDB");

            Results = new ObservableCollection<ResultKind>();
        }

        public void LoadData()
        {
            sqlSentence = "EXEC usp_hqms_getbasj '" + BeginDate.ToString("yyyyMMdd") + "','" + EndDate.ToString("yyyyMMdd") + "','01'";
            bagldbController.Query<ResultKind>(sqlSentence, out resultHub);

            CurrentPage = 1;

            TotalRecordCount = resultHub.Count;
            DisplayRecordPage();
        }

        public void DisplayRecordPage()
        {
            Results.Clear();

            int currentRecordCount = (CurrentPage - 1) * PageRecordCount;

            Results.AddRange(resultHub.GetRange(currentRecordCount, (TotalRecordCount - currentRecordCount) / PageRecordCount > 0 ? PageRecordCount : (TotalRecordCount - currentRecordCount) % PageRecordCount));
        }

        public void ExportData()
        {
            string csvFilePath = @"E:\Work\Code\HQMS\file.csv";

            using (StreamWriter writer = new StreamWriter(csvFilePath))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(resultHub);
            }
        }

        public void UpLoadData()
        {
            string UpLoadPath = @"E:\Work\Code\file.csv";

            File.Copy(@"E:\Work\Code\HQMS\file.csv", UpLoadPath, true);
        }
    }
}

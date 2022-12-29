using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;
using Npoi.Mapper;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Control.Extension.PerformanceAssessment.Models
{
    public class DataExportingModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController bagldbController;

        string sqlSentence, sqlRetSentence;
        List<MasterKind> masterHub;
        List<DetailKind> detailHub;

        string masterExportXlsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\master.xls";
        string detailExportXlsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\detail.xls";

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

        MasterKind currentMaster;
        public MasterKind CurrentMaster
        {
            get => currentMaster;
            set => SetProperty(ref currentMaster, value);
        }

        ObservableCollection<MasterKind> masters;
        public ObservableCollection<MasterKind> Masters
        {
            get => masters;
            set => SetProperty(ref masters, value);
        }

        ObservableCollection<DetailKind> details;
        public ObservableCollection<DetailKind> Details
        {
            get => details;
            set => SetProperty(ref details, value);
        }

        public DataExportingModel(IContainerProvider containerProviderArgs)
        {
            messageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            bagldbController = environmentMonitor.DataBaseSetting.GetContent("BAGLDB");

            Masters = new ObservableCollection<MasterKind>();
            Details = new ObservableCollection<DetailKind>();
        }

        public void LoadMasterData()
        {
            Masters.Clear();

            sqlSentence = "EXEC usp_hqms_gettjsj '" + BeginDate.ToString("yyyyMMdd") + "','" + EndDate.ToString("yyyyMMdd") + "','1'";

            if (bagldbController.QueryWithMessage<MasterKind>(sqlSentence, out masterHub, out sqlRetSentence))
                Masters.AddRange(masterHub);
            else
                messageQueue.Enqueue(sqlRetSentence);
        }

        public void LoadDetailData()
        {
            Details.Clear();

            sqlSentence = "EXEC usp_hqms_gettjsj '" + BeginDate.ToString("yyyyMMdd") + "','" + EndDate.ToString("yyyyMMdd") + "','0','"+ CurrentMaster.FCODE+"'";

            if (bagldbController.QueryWithMessage<DetailKind>(sqlSentence, out detailHub, out sqlRetSentence))
                Details.AddRange(detailHub);
            else
                messageQueue.Enqueue(sqlRetSentence);
        }
        
        public void ExprotMasterData()
        {
            Mapper mapper = new Mapper();
            mapper.Save(masterExportXlsFilePath, Masters, sheetIndex: 1, overwrite: true, xlsx: false);
        }

        public void ExprotDetailData()
        {
            Mapper mapper = new Mapper();
            mapper.Save(detailExportXlsFilePath, Details, sheetIndex: 1, overwrite: true, xlsx: false);
        }
    }
}

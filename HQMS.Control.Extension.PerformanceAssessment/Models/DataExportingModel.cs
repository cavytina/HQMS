using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Ioc;
using Prism.Mvvm;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Control.Extension.PerformanceAssessment.Models
{
    public class DataExportingModel : BindableBase
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController bagldbController;

        string sqlSentence;
        List<MasterKind> masterHub;
        List<DetailKind> detailHub;

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
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            bagldbController = environmentMonitor.DataBaseSetting.GetContent("BAGLDB");

            Masters = new ObservableCollection<MasterKind>();
            Details = new ObservableCollection<DetailKind>();
        }

        public void LoadMasterData()
        {
            Masters.Clear();

            sqlSentence = "EXEC usp_hqms_gettjsj '" + BeginDate.ToString("yyyyMMdd") + "','" + EndDate.ToString("yyyyMMdd") + "'";
            bagldbController.Query<MasterKind>(sqlSentence, out masterHub);

            Masters.AddRange(masterHub);
        }

        public void LoadDetailData()
        {

        }
    }
}

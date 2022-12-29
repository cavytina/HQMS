using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Control.Extension.PerformanceAssessment.Models
{
    public class DataMappingModel : BindableBase
    {
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController BAGLDBController;
        string sqlStatement;
        object retObj;
        List<CategoryKind> categoryHub;
        List<LocalKind> localHub;
        List<StandardKind> standardHub;
        List<MatchedKind> matchedHub;

        ObservableCollection<CategoryKind> categorys;
        public ObservableCollection<CategoryKind> Categorys
        {
            get => categorys;
            set => SetProperty(ref categorys, value);
        }

        CategoryKind currentCategory;
        public CategoryKind CurrentCategory
        {
            get => currentCategory;
            set => SetProperty(ref currentCategory, value);
        }

        ObservableCollection<LocalKind> locals;
        public ObservableCollection<LocalKind> Locals
        {
            get => locals;
            set => SetProperty(ref locals, value);
        }

        LocalKind currentLocal;
        public LocalKind CurrentLocal
        {
            get => currentLocal;
            set => SetProperty(ref currentLocal, value);
        }

        ObservableCollection<StandardKind> standards;
        public ObservableCollection<StandardKind> Standards
        {
            get => standards;
            set => SetProperty(ref standards, value);
        }

        StandardKind currentStandard;
        public StandardKind CurrentStandard
        {
            get => currentStandard;
            set => SetProperty(ref currentStandard, value);
        }

        ObservableCollection<MatchedKind> matcheds;
        public ObservableCollection<MatchedKind> Matcheds
        {
            get => matcheds;
            set => SetProperty(ref matcheds, value);
        }

        MatchedKind currentMatched;
        public MatchedKind CurrentMatched
        {
            get => currentMatched;
            set => SetProperty(ref currentMatched, value);
        }

        public DataMappingModel (IContainerProvider containerProviderArgs)
        {
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            BAGLDBController = environmentMonitor.DataBaseSetting.GetContent("BAGLDB");

            Categorys = new ObservableCollection<CategoryKind>();
            Locals = new ObservableCollection<LocalKind>();
            Standards = new ObservableCollection<StandardKind>();
            Matcheds = new ObservableCollection<MatchedKind>();
        }

        public bool LoadCategoryData()
        {
            bool ret = false;

            sqlStatement = "SELECT CategoryCode,CategoryName FROM dbo.tf_hqms_getppsj('Category','')";
            if (BAGLDBController.Query<CategoryKind>(sqlStatement, out categoryHub))
            {
                Categorys.AddRange(categoryHub);
                ret = true;
            }

            return ret;
        }

        public bool RefreshCategoryData()
        {
            bool ret = false;

            sqlStatement = "SELECT CategoryCode,CategoryName,LocalCode,LocalName from dbo.tf_hqms_getppsj('Local','" + CurrentCategory.CategoryCode + "')";
            if (BAGLDBController.Query<LocalKind>(sqlStatement,out localHub))
            {
                Locals.AddRange(localHub);

                sqlStatement = "SELECT CategoryCode,CategoryName,StandardCode,StandardName from dbo.tf_hqms_getppsj('Standard','" + CurrentCategory.CategoryCode + "')";
                if (BAGLDBController.Query<StandardKind>(sqlStatement, out standardHub))
                {
                    Standards.AddRange(standardHub);

                    sqlStatement = "SELECT CategoryCode,CategoryName,LocalCode,LocalName,StandardCode,StandardName from dbo.tf_hqms_getppsj('Matched','" + CurrentCategory.CategoryCode + "')";
                    if (BAGLDBController.Query<MatchedKind>(sqlStatement, out matchedHub))
                    {
                        Matcheds.AddRange(matchedHub);
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public bool MatchData()
        {
            bool ret = false;

            sqlStatement = "exec usp_hqms_getppsj 'Match','" + CurrentCategory.CategoryCode + "','" + CurrentCategory.CategoryName + "','" +
                            CurrentLocal.LocalCode + "','" + CurrentLocal.LocalName + "','" + CurrentStandard.StandardCode + "','" + CurrentStandard.StandardName + "'";
            if (BAGLDBController.ExecuteScalar(sqlStatement, out retObj))
            {
                if (retObj.ToString() == "T")
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool CancelMatchData()
        {
            bool ret = false;

            sqlStatement = "exec usp_hqms_getppsj 'UnMatch','" + CurrentCategory.CategoryCode + "','" + CurrentCategory.CategoryName + "','" +
                            CurrentMatched.LocalCode + "','" + CurrentMatched.LocalName + "','" + CurrentMatched.StandardCode + "','" + CurrentMatched.StandardName + "'";
            if (BAGLDBController.ExecuteScalar(sqlStatement, out retObj))
            {
                if (retObj.ToString() == "T")
                {
                    ret = true;
                }
            }

            return ret;
        }
    }
}

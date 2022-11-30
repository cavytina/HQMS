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
        }

        public bool LoadCategoryData()
        {
            bool ret = false;

            sqlStatement = "select CategoryCode,CategoryName from dbo.TF_HQMS_LoadCategoryInfo('Category','')";
            if (BAGLDBController.Query<CategoryKind>(sqlStatement, out categoryHub))
            {
                Categorys = new ObservableCollection<CategoryKind>(categoryHub);
                ret = true;
            }

            return ret;
        }

        public bool RefreshCategoryData()
        {
            bool ret = false;

            sqlStatement = "select CategoryCode,CategoryName,LocalCode,LocalName from dbo.TF_HQMS_LoadCategoryInfo('Local','" + CurrentCategory.CategoryCode + "')";
            if (BAGLDBController.Query<LocalKind>(sqlStatement,out localHub))
            {
                Locals = new ObservableCollection<LocalKind>(localHub);

                sqlStatement = "select CategoryCode,CategoryName,StandardCode,StandardName from dbo.TF_HQMS_LoadCategoryInfo('Standard','" + CurrentCategory.CategoryCode + "')";
                if (BAGLDBController.Query<StandardKind>(sqlStatement, out standardHub))
                {
                    Standards = new ObservableCollection<StandardKind>(standardHub);

                    sqlStatement = "select CategoryCode,CategoryName,LocalCode,LocalName,StandardCode,StandardName from dbo.TF_HQMS_LoadCategoryInfo('Matched','" + CurrentCategory.CategoryCode + "')";
                    if (BAGLDBController.Query<MatchedKind>(sqlStatement, out matchedHub))
                    {
                        Matcheds = new ObservableCollection<MatchedKind>(matchedHub);
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public bool MatchData()
        {
            bool ret = false;

            sqlStatement = "exec USP_HQMS_MatchCategoryInfo 'Match','" + CurrentCategory.CategoryCode + "','" + CurrentCategory.CategoryName + "','" +
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

            sqlStatement = "exec USP_HQMS_MatchCategoryInfo 'UnMatch','" + CurrentCategory.CategoryCode + "','" + CurrentCategory.CategoryName + "','" +
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

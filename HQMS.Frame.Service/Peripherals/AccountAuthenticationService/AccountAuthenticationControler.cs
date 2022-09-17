using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Services;
using HQMS.Frame.Kernel.Environment;
using HQMS.Frame.Service.Infrastructure;
using HQMS.Frame.Service.Peripherals;
using System.Windows;

namespace HQMS.Frame.Service.Peripherals
{
    public class AccountAuthenticationControler : IAccountAuthenticationControler
    {
        IContainerProvider containerProvider;
        IEnvironmentMonitor environmentMonitor;
        IDataBaseController bagldbController;
        string sqlStatement;
        List<tsysuserKind> tsysuserHub;

        public AccountAuthenticationControler(IContainerProvider containerProviderArgs)
        {
            containerProvider = containerProviderArgs;
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            bagldbController = environmentMonitor.DataBaseSetting.GetDataBaseController("BAGLDB");
        }

        public bool Validate(AccountInfoKind accountInfoKindArgs)
        {
            string pwdFromDBText, pwdCipherText;

            sqlStatement = "SELECT FCODE,FNAME,FPASSWORD FROM TSYSUSER";
            bagldbController.Query<tsysuserKind>(sqlStatement, out tsysuserHub);
            var currentTsysuser = from currentTsysuserHub in tsysuserHub
                                  where currentTsysuserHub.FCODE == accountInfoKindArgs.account
                                  select currentTsysuserHub;
            pwdFromDBText = currentTsysuser.FirstOrDefault().FPASSWORD;

            IOneWayCipherController oneWayCipherController = containerProvider.Resolve<IOneWayCipherController>();
            pwdCipherText = oneWayCipherController.Encrypt(accountInfoKindArgs.password);

            if (pwdFromDBText == pwdCipherText)
                return true;
            else
                return false;
        }
    }
}

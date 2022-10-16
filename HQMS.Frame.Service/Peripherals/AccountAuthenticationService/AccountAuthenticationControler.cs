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

        public bool Validate(RequestAccountKind accountInfoArgs, out string messageArgs)
        {
            bool ret = false;
            string pwdFromDBText,pwdCipherText;
            messageArgs = string.Empty;

            sqlStatement = "SELECT FCODE,FNAME,FPASSWORD FROM TSYSUSER";
            bagldbController.Query<tsysuserKind>(sqlStatement, out tsysuserHub);
            var currentTsysuser = from currentTsysuserHub in tsysuserHub
                                  where currentTsysuserHub.FCODE == accountInfoArgs.Account
                                  select currentTsysuserHub;
            pwdFromDBText = currentTsysuser.FirstOrDefault().FPASSWORD;

            IOneWayCipherController oneWayCipherController = containerProvider.Resolve<IOneWayCipherController>();
            pwdCipherText = oneWayCipherController.Encrypt(accountInfoArgs.Password);

            if (pwdFromDBText == null)
                messageArgs = "无此账户记录";
            else if (pwdFromDBText != pwdCipherText)
                messageArgs = "密码错误,请核对后重新输入";
            else if (pwdFromDBText == pwdCipherText)
            {
                ret = true;
                messageArgs = currentTsysuser.FirstOrDefault().FNAME;
            }

            return ret;
        }
    }
}

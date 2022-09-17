using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json;
using HQMS.Frame.Kernel.Infrastructure;
using HQMS.Frame.Kernel.Events;

namespace HQMS.Frame.Control.Login.Models
{
    public class ContentModel : BindableBase
    {
        IEventAggregator eventAggregator;
        bool ret;

        string account;
        public string Account
        {
            get => account;
            set => SetProperty(ref account, value);
        }

        string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ContentModel(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();

            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnResponseService);
        }

        private void OnResponseService(string responseServiceText)
        {
            ServiceKind responseService = JsonConvert.DeserializeObject<ServiceKind>(responseServiceText);

            if (responseService.name == "AccountAuthenticationService")
            {
                ResultKind resultInfo = JsonConvert.DeserializeObject<ResultKind>(responseService.content);

                if (resultInfo.result == "True")
                    ret = true;
            }
        }

        public bool IsLoginSucceed()
        {
            ret = false;

            AccountInfoKind accountInfo = new AccountInfoKind
            {
                account = Account,
                password = Password
            };

            ServiceKind requestAccountAuthenticationService = new ServiceKind
            {
                code = "01",
                name = "AccountAuthenticationService",  
                content = JsonConvert.SerializeObject(accountInfo)
            };

            string requestAccountAuthenticationServiceJsonText = JsonConvert.SerializeObject(requestAccountAuthenticationService);

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(requestAccountAuthenticationServiceJsonText);

            return ret;
        }
    }
}

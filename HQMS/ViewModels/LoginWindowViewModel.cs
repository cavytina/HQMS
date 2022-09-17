using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using MaterialDesignThemes.Wpf;

namespace HQMS.ViewModels
{
    public class LoginWindowViewModel:BindableBase
    {
        ISnackbarMessageQueue messageQueue;
        public ISnackbarMessageQueue LoginMessageQueue
        {
            get => messageQueue;
            set => SetProperty(ref messageQueue, value);
        }

        public LoginWindowViewModel(IContainerProvider containerProviderArgs)
        {
            LoginMessageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();
        }
    }
}

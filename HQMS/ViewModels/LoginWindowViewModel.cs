using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using MaterialDesignThemes.Wpf;
using HQMS.Models;

namespace HQMS.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        LoginWindowModel loginWindowModel;
        public LoginWindowModel LoginWindowModel
        {
            get => loginWindowModel;
            set => SetProperty(ref loginWindowModel, value);
        }

        ISnackbarMessageQueue messageQueue;
        public ISnackbarMessageQueue LoginMessageQueue
        {
            get => messageQueue;
            set => SetProperty(ref messageQueue, value);
        }

        public LoginWindowViewModel(IContainerProvider containerProviderArgs)
        {
            LoginWindowModel = new LoginWindowModel(containerProviderArgs);

            LoginMessageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();
        }
    }
}

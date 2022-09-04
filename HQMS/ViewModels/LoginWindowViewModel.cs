using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;
using Prism.Ioc;
using Prism.Commands;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using HQMS.Frame.Kernel.Core;
using HQMS.Frame.Kernel.Events;
using HQMS.Views;

namespace HQMS.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        IEventAggregator eventAggregator;

        ISnackbarMessageQueue loginMessageQueue;
        public ISnackbarMessageQueue LoginMessageQueue
        {
            get => loginMessageQueue;
            set => SetProperty(ref loginMessageQueue, value);
        }

        public DelegateCommand WindowLoadedCommand { get; private set; }
        public DelegateCommand WindowClosedCommand { get; private set; }

        public LoginWindowViewModel(IContainerProvider containerProviderArgs, IEventAggregator eventAggregatorArgs)
        {
            eventAggregator = eventAggregatorArgs;

            LoginMessageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();

            WindowLoadedCommand = new DelegateCommand(OnWindowLoaded);
            WindowClosedCommand = new DelegateCommand(OnWindowClosed);
        }

        private void OnWindowLoaded()
        {
            eventAggregator.GetEvent<WindowStatusChangedEvent>().Publish(WindowStatusPart.LoginWindowLoaded);
        }

        private void OnWindowClosed()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using MaterialDesignThemes.Wpf;
using HQMS.Frame.Control.Login.Models;

namespace HQMS.Frame.Control.Login.ViewModels
{
    public class LoginContentViewModel : BindableBase
    {
        ISnackbarMessageQueue messageQueue;

        LoginContentModel loginContentModel;
        public LoginContentModel LoginContentModel
        {
            get => loginContentModel;
            set => SetProperty(ref loginContentModel, value);
        }

        public DelegateCommand<object> LoginCommand { get; private set; }

        public LoginContentViewModel(IContainerProvider containerProviderArgs)
        {
            LoginContentModel = new LoginContentModel(containerProviderArgs);

            messageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();

            LoginCommand = new DelegateCommand<object>(OnLogin);
        }

        private void OnLogin(object obj)
        {
            if (!LoginContentModel.IsLoginSucceed())
                messageQueue.Enqueue("登录失败!");
            else
            {
                SystemCommands.CloseWindow(obj as Window);
                LoginContentModel.NavigateToMainWindow();
            }

        }
    }
}

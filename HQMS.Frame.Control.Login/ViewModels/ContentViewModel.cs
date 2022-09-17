using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using MaterialDesignThemes.Wpf;
using HQMS.Frame.Control.Login.Models;

namespace HQMS.Frame.Control.Login.ViewModels
{
    public class ContentViewModel:BindableBase
    {
        ISnackbarMessageQueue messageQueue;

        ContentModel contentModel;
        public ContentModel ContentModel
        {
            get => contentModel;
            set => SetProperty(ref contentModel, value);
        }

        public DelegateCommand<object> LoginCommand { get; private set; }

        public ContentViewModel(IContainerProvider containerProviderArgs)
        {
            ContentModel = new ContentModel(containerProviderArgs);

            messageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();

            LoginCommand = new DelegateCommand<object>(OnLogin);
        }

        private void OnLogin(object obj)
        {
            if (!ContentModel.IsLoginSucceed())
                messageQueue.Enqueue("登录失败!");
        }
    }
}

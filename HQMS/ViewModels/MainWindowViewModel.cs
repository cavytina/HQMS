using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using MaterialDesignThemes.Wpf;
using HQMS.Models;

namespace HQMS.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        double workAreaWidth;
        public double WorkAreaWidth
        {
            get => workAreaWidth;
            set => SetProperty(ref workAreaWidth, value);
        }

        double workAreaHeight;
        public double WorkAreaHeight
        {
            get => workAreaHeight;
            set => SetProperty(ref workAreaHeight, value);
        }

        ISnackbarMessageQueue messageQueue;
        public ISnackbarMessageQueue MessageQueue
        {
            get => messageQueue;
            set => SetProperty(ref messageQueue, value);
        }

        IRegionManager regionManager;
        public IRegionManager RegionManager
        {
            get => regionManager;
            set => SetProperty(ref regionManager, value);
        }

        MainWindowModel mainWindowModel;
        public MainWindowModel MainWindowModel
        {
            get => mainWindowModel;
            set => SetProperty(ref mainWindowModel, value);
        }

        public MainWindowViewModel(IContainerProvider containerProviderArgs)
        {
            MainWindowModel = new MainWindowModel(containerProviderArgs);

            WorkAreaWidth = SystemParameters.WorkArea.Width;
            WorkAreaHeight = SystemParameters.WorkArea.Height;

            MessageQueue = containerProviderArgs.Resolve<ISnackbarMessageQueue>();
            RegionManager = containerProviderArgs.Resolve<IRegionManager>();
        }
    }
}

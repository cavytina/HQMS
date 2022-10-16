using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HQMS.Frame.Control.MainLeftDrawer.Models;

namespace HQMS.Frame.Control.MainLeftDrawer.ViewModels
{
    public class MainLeftDrawerViewModel : BindableBase
    {
        MainLeftDrawerModel mainLeftDrawerModel;
        public MainLeftDrawerModel MainLeftDrawerModel
        {
            get => mainLeftDrawerModel;
            set => SetProperty(ref mainLeftDrawerModel, value);
        }

        public DelegateCommand WindowLoadedCommand { get; private set; }

        public MainLeftDrawerViewModel(IContainerProvider containerProviderArgs)
        {
            MainLeftDrawerModel = new MainLeftDrawerModel(containerProviderArgs);
            WindowLoadedCommand = new DelegateCommand(OnWindowLoaded);
        }

        private void OnWindowLoaded()
        {
            MainLeftDrawerModel.RequestMenuItemData();
            MainLeftDrawerModel.LoadTopLevelMenuItemData();
        }
    }
}

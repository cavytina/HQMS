using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
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
        }
    }
}

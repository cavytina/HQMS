﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;

namespace HQMS.ViewModels
{
    public class MainWindowModel:BindableBase
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

        public MainWindowModel(IContainerProvider containerProvider)
        {
            WorkAreaWidth = SystemParameters.WorkArea.Width;
            WorkAreaHeight = SystemParameters.WorkArea.Height;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using MaterialDesignThemes.Wpf;
using HAMS.Models;

namespace HAMS.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        MainWindowModel mainWindowModel;
        public MainWindowModel MainWindowModel
        {
            get => mainWindowModel;
            set => SetProperty(ref mainWindowModel, value);
        }

        public MainWindowViewModel(IContainerProvider containerProviderArg)
        {
            MainWindowModel = new MainWindowModel(containerProviderArg);
        }
    }
}

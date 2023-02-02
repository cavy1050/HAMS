using System;
using Prism.Ioc;
using Prism.Mvvm;
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

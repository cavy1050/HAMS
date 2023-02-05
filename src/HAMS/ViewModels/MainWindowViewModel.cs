using System;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
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

        public DelegateCommand WindowLoadedCommand { get; private set; }

        public MainWindowViewModel(IContainerProvider containerProviderArg)
        {
            MainWindowModel = new MainWindowModel(containerProviderArg);
            WindowLoadedCommand = new DelegateCommand(OnWindowLoaded);
        }

        private void OnWindowLoaded()
        {
            MainWindowModel.WindowLoaded();
        }
    }
}

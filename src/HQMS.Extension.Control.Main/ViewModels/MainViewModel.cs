using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using HQMS.Extension.Control.Main.Models;

namespace HQMS.Extension.Control.Main.ViewModels
{
    public class MainViewModel : BindableBase
    {
        IRegionManager regionManager;

        string extensionModuleName = "绩效考核";
        public string ExtensionModuleName
        {
            get => extensionModuleName;
            set => SetProperty(ref extensionModuleName, value);
        }

        MainModel mainModel;
        public MainModel MainModel
        {
            get => mainModel;
            set => SetProperty(ref mainModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand MenuSelectedChangedCommand { get; private set; }

        public MainViewModel(IContainerProvider containerProviderArg)
        {
            MainModel = new MainModel(containerProviderArg);

            regionManager = containerProviderArg.Resolve<IRegionManager>();

            LoadedCommand = new DelegateCommand(OnLoaded);
            MenuSelectedChangedCommand = new DelegateCommand(OnMenuSelectedChanged);
        }

        private void OnLoaded()
        {
            MainModel.Loaded();
        }

        private void OnMenuSelectedChanged()
        {
            switch (MainModel.CurrentItem)
            {
                case "DataMapping": regionManager.RequestNavigate("HQMS.MainContentRegion", "DataMappingView"); break;
                case "DataQuerying": regionManager.RequestNavigate("HQMS.MainContentRegion", "DataQueryingView"); break;
                case "DataExporting": regionManager.RequestNavigate("HQMS.MainContentRegion", "DataExportingView"); break;
            }
        }
    }
}

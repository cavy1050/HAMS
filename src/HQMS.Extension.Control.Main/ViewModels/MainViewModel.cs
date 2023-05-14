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

        public DelegateCommand DataMatchingCheckedCommand { get; private set; }
        public DelegateCommand DataQueryingCheckedCommand { get; private set; }
        public DelegateCommand DataExportingCheckedCommand { get; private set; }

        public MainViewModel(IContainerProvider containerProviderArg)
        {
            regionManager = containerProviderArg.Resolve<IRegionManager>();

            DataMatchingCheckedCommand = new DelegateCommand(OnDataMatchingChecked);
            DataQueryingCheckedCommand = new DelegateCommand(OnDataQueryingChecked);
            DataExportingCheckedCommand = new DelegateCommand(OnDataExportingChecked);
        }

        private void OnDataMatchingChecked()
        {
            regionManager.RequestNavigate("HQMS.MainContentRegion", "DataMappingView");
        }

        private void OnDataQueryingChecked()
        {
            regionManager.RequestNavigate("HQMS.MainContentRegion", "DataQueryingView");
        }

        private void OnDataExportingChecked()
        {
            regionManager.RequestNavigate("HQMS.MainContentRegion", "DataExportingView");
        }
    }
}

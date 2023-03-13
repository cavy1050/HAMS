using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HQMS.Extension.Control.Main.Models;

namespace HQMS.Extension.Control.Main.ViewModels
{
    public class DataExportingViewModel : BindableBase
    {
        DataExportingModel dataExportingModel;
        public DataExportingModel DataExportingModel
        {
            get => dataExportingModel;
            set => SetProperty(ref dataExportingModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand MasterCommand { get; private set; }
        public DelegateCommand DetailCommand { get; private set; }
        public DelegateCommand MasterExportCommand { get; private set; }
        public DelegateCommand DetailExportCommand { get; private set; }

        public DataExportingViewModel(IContainerProvider containerProviderArg)
        {
            DataExportingModel = new DataExportingModel(containerProviderArg);

            LoadedCommand = new DelegateCommand(OnLoaded);
            MasterCommand = new DelegateCommand(OnMaster);
            DetailCommand = new DelegateCommand(OnDetail);
            MasterExportCommand = new DelegateCommand(OnMasterExport);
            DetailExportCommand = new DelegateCommand(OnDetailExport);
        }

        private void OnLoaded()
        {
            DataExportingModel.Loaded();
        }

        private void OnMaster()
        {
            DataExportingModel.LoadMasterData();
        }

        private void OnDetail()
        {
            DataExportingModel.LoadDetailData();
        }

        private void OnMasterExport()
        {
            DataExportingModel.ExprotMasterData();
        }

        private void OnDetailExport()
        {
            DataExportingModel.ExprotDetailData();
        }
    }
}

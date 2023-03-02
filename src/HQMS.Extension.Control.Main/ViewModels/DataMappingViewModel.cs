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
    public class DataMappingViewModel : BindableBase
    {
        DataMappingModel dataMappingModel;
        public DataMappingModel DataMappingModel
        {
            get => dataMappingModel;
            set => SetProperty(ref dataMappingModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand CatalogChangedCommand { get; private set; }

        public DelegateCommand MatchCommand { get; private set; }
        public DelegateCommand CancelMatchCommand { get; private set; }

        public DataMappingViewModel(IContainerProvider containerProviderArgs)
        {
            DataMappingModel = new DataMappingModel(containerProviderArgs);

            LoadedCommand = new DelegateCommand(OnLoaded);
            CatalogChangedCommand = new DelegateCommand(OnCatalogChanged);
            MatchCommand = new DelegateCommand(OnMatch);
            CancelMatchCommand = new DelegateCommand(OnCancelMatch);
        }

        private void OnLoaded()
        {
            DataMappingModel.Load();
        }

        private void OnCatalogChanged()
        {
            DataMappingModel.RefreshCatalogData();
        }

        private void OnMatch()
        {
            DataMappingModel.MatchData();
        }

        private void OnCancelMatch()
        {
            DataMappingModel.CancelMatchData();
        }
    }
}

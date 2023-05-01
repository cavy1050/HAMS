using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using IDMS.Extension.Control.InfluenzaSurveillance.Models;

namespace IDMS.Extension.Control.InfluenzaSurveillance.ViewModels
{
    public class InfluenzaSurveillanceViewModel : BindableBase
    {
        string extensionModuleName = "流感监测";
        public string ExtensionModuleName
        {
            get => extensionModuleName;
            set => SetProperty(ref extensionModuleName, value);
        }

        InfluenzaSurveillanceModel influenzaSurveillanceModel;
        public InfluenzaSurveillanceModel InfluenzaSurveillanceModel
        {
            get => influenzaSurveillanceModel;
            set => SetProperty(ref influenzaSurveillanceModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand QueryCommand { get; private set; }
        public DelegateCommand ExportCommand { get; private set; }

        public DelegateCommand NavigateFirstPageCommand { get; private set; }
        public DelegateCommand NavigateBeforePageCommand { get; private set; }
        public DelegateCommand NavigateNextPageCommand { get; private set; }
        public DelegateCommand NavigateLastPageCommand { get; private set; }
        public DelegateCommand NavigateCurrentPageCommand { get; private set; }

        public InfluenzaSurveillanceViewModel (IContainerProvider containerProviderArg)
        {
            InfluenzaSurveillanceModel = new InfluenzaSurveillanceModel(containerProviderArg);

            LoadedCommand = new DelegateCommand(OnLoaded);
            QueryCommand = new DelegateCommand(OnQuery);
            ExportCommand = new DelegateCommand(OnExport);

            NavigateFirstPageCommand = new DelegateCommand(OnNavigateFirstPage);
            NavigateBeforePageCommand = new DelegateCommand(OnNavigateBeforePage);
            NavigateNextPageCommand = new DelegateCommand(OnNavigateNextPage);
            NavigateLastPageCommand = new DelegateCommand(OnNavigateLastPage);
            NavigateCurrentPageCommand = new DelegateCommand(OnNavigateCurrentPage);
        }

        private void OnLoaded()
        {
            InfluenzaSurveillanceModel.Loaded();
        }

        private void OnQuery()
        {
            InfluenzaSurveillanceModel.QueryData();
        }

        private void OnExport()
        {
            InfluenzaSurveillanceModel.ExportData();
        }

        private void OnNavigateFirstPage()
        {
            InfluenzaSurveillanceModel.NavigateFirstPage();
        }

        private void OnNavigateBeforePage()
        {
            InfluenzaSurveillanceModel.NavigateBeforePage();
        }

        private void OnNavigateNextPage()
        {
            InfluenzaSurveillanceModel.NavigateNextPage();
        }

        private void OnNavigateLastPage()
        {
            InfluenzaSurveillanceModel.NavigateLastPage();
        }

        private void OnNavigateCurrentPage()
        {
            InfluenzaSurveillanceModel.NavigateCurrentPage();
        }
    }
}

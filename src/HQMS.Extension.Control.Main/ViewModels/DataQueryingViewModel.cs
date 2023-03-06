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
    public class DataQueryingViewModel : BindableBase
    {
        DataQueryingModel dataQueryingModel;
        public DataQueryingModel DataQueryingModel
        {
            get => dataQueryingModel;
            set => SetProperty(ref dataQueryingModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand QueryCommand { get; private set; }
        public DelegateCommand ExportCommand { get; private set; }
        public DelegateCommand UpLoadCommand { get; private set; }

        public DelegateCommand NavigateFirstPageCommand { get; private set; }
        public DelegateCommand NavigateBeforePageCommand { get; private set; }
        public DelegateCommand NavigateNextPageCommand { get; private set; }
        public DelegateCommand NavigateLastPageCommand { get; private set; }
        public DelegateCommand NavigateCurrentPageCommand { get; private set; }

        public DataQueryingViewModel(IContainerProvider containerProviderArg)
        {
            DataQueryingModel = new DataQueryingModel(containerProviderArg);

            LoadedCommand = new DelegateCommand(OnLoaded);
            QueryCommand = new DelegateCommand(OnQuery);
            ExportCommand = new DelegateCommand(OnExport);
            UpLoadCommand = new DelegateCommand(OnUpLoad);

            NavigateFirstPageCommand = new DelegateCommand(OnNavigateFirstPage);
            NavigateBeforePageCommand = new DelegateCommand(OnNavigateBeforePage);
            NavigateNextPageCommand = new DelegateCommand(OnNavigateNextPage);
            NavigateLastPageCommand = new DelegateCommand(OnNavigateLastPage);
            NavigateCurrentPageCommand = new DelegateCommand(OnNavigateCurrentPage);
        }

        private void OnLoaded()
        {
            DataQueryingModel.Loaded();
        }

        private void OnQuery()
        {
            DataQueryingModel.QueryData();
        }

        private void OnExport()
        {
            DataQueryingModel.ExportData();
        }

        private void OnUpLoad()
        {
            DataQueryingModel.UpLoadData();
        }

        private void OnNavigateFirstPage()
        {
            DataQueryingModel.NavigateFirstPage();
        }

        private void OnNavigateBeforePage()
        {
            DataQueryingModel.NavigateBeforePage();
        }

        private void OnNavigateNextPage()
        {
            DataQueryingModel.NavigateNextPage();
        }

        private void OnNavigateLastPage()
        {
            DataQueryingModel.NavigateLastPage();
        }

        private void OnNavigateCurrentPage()
        {
            DataQueryingModel.NavigateCurrentPage();
        }
    }
}

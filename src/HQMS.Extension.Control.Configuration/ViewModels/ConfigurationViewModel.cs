using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HQMS.Extension.Control.Configuration.Models;

namespace HQMS.Extension.Control.Configuration.ViewModels
{
    public class ConfigurationViewModel : BindableBase
    {
        string extensionModuleName = "绩效考核设置";
        public string ExtensionModuleName
        {
            get => extensionModuleName;
            set => SetProperty(ref extensionModuleName, value);
        }

        ConfigurationModel configurationModel;
        public ConfigurationModel ConfigurationModel
        {
            get => configurationModel;
            set => SetProperty(ref configurationModel, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand OpenExportFileCatalogueCommand { get; private set; }
        public DelegateCommand OpenUpLoadFileCatalogueCommand { get; private set; }
        public DelegateCommand OpenMasterExportFileCatalogueCommand { get; private set; }
        public DelegateCommand OpenDetailExportFileCatalogueCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        public ConfigurationViewModel(IContainerProvider containerProviderArg)
        {
            ConfigurationModel = new ConfigurationModel(containerProviderArg);

            LoadedCommand = new DelegateCommand(OnLoaded);
            OpenExportFileCatalogueCommand = new DelegateCommand(OnOpenExportFileCatalogue);
            OpenUpLoadFileCatalogueCommand = new DelegateCommand(OnOpenUpLoadFileCatalogue);
            OpenMasterExportFileCatalogueCommand = new DelegateCommand(OnOpenMasterExportFileCatalogue);
            OpenDetailExportFileCatalogueCommand = new DelegateCommand(OnOpenDetailExportFileCatalogue);
            SaveCommand = new DelegateCommand(OnSave);
        }

        private void OnLoaded()
        {
            ConfigurationModel.Loaded();
        }

        private void OnOpenExportFileCatalogue()
        {
            ConfigurationModel.OpenExportFileCatalogue();
        }

        private void OnOpenUpLoadFileCatalogue()
        {
            ConfigurationModel.OpenUpLoadFileCatalogue();
        }

        private void OnOpenMasterExportFileCatalogue()
        {
            ConfigurationModel.OpenMasterExportFileCatalogue();
        }

        private void OnOpenDetailExportFileCatalogue()
        {
            ConfigurationModel.OpenDetailExportFileCatalogue();
        }

        private void OnSave()
        {
            ConfigurationModel.Save();
        }
    }
}

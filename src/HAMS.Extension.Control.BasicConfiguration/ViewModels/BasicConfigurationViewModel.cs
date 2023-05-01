using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HAMS.Extension.Control.BasicConfiguration.Models;

namespace HAMS.Extension.Control.BasicConfiguration.ViewModels
{
    public class BasicConfigurationViewModel : BindableBase
    {
        string extensionModuleName = "基本设置";
        public string ExtensionModuleName
        {
            get => extensionModuleName;
            set => SetProperty(ref extensionModuleName, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand<object> OpenFileCatalogueCommand { get; private set; }
        public DelegateCommand<object> ConnectionCommand { get; private set; }
        public DelegateCommand<object> ConnectStringSettingCommand { get; private set; }
        public DelegateCommand BAGLDBConnectionCommand { get; private set; }
        public DelegateCommand DefaultCommand { get; private set; }
        public DelegateCommand ApplyCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        BasicConfigurationModel basicConfigurationModel;
        public BasicConfigurationModel BasicConfigurationModel
        {
            get => basicConfigurationModel;
            set => SetProperty(ref basicConfigurationModel, value);
        }

        public BasicConfigurationViewModel(IContainerProvider containerProviderArg)
        {
            BasicConfigurationModel = new BasicConfigurationModel(containerProviderArg);

            LoadedCommand = new DelegateCommand(OnLoaded);
            OpenFileCatalogueCommand = new DelegateCommand<object>(OnOpenFileCatalogue);
            ConnectionCommand = new DelegateCommand<object>(OnConnection);
            ConnectStringSettingCommand = new DelegateCommand<object>(OnConnectStringSetting);
            DefaultCommand = new DelegateCommand(OnDefault);
            ApplyCommand = new DelegateCommand(OnApply);
            SaveCommand = new DelegateCommand(OnSave);
        }

        private void OnLoaded()
        {
            BasicConfigurationModel.OnLoaded();
        }

        private void OnOpenFileCatalogue(object obj)
        {
            string fileCatalogueIdentifierArg = (string)obj;
            BasicConfigurationModel.OnOpenFileCatalogue(fileCatalogueIdentifierArg);
        }

        private void OnConnection(object obj)
        {
            string dataBaseIdentifierArg = (string)obj;
            BasicConfigurationModel.Connection(dataBaseIdentifierArg);
        }

        private void OnConnectStringSetting(object obj)
        {
            string dataBaseIdentifierArg = (string)obj;
            BasicConfigurationModel.ConnectStringSetting(dataBaseIdentifierArg);
        }

        private void OnDefault()
        {
            BasicConfigurationModel.OnDefault();
        }

        private void OnApply()
        {
            BasicConfigurationModel.OnApply();
        }

        private void OnSave()
        {
            BasicConfigurationModel.OnSave();
        }
    }
}

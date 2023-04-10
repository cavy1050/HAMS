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
        public DelegateCommand OpenLogFileCatalogueCommand { get; private set; }
        public DelegateCommand NativeDBConnectionCommand { get; private set; }
        public DelegateCommand BAGLDBConnectStringCommand { get; private set; }
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
            OpenLogFileCatalogueCommand = new DelegateCommand(OnOpenLogFileCatalogue);
            NativeDBConnectionCommand = new DelegateCommand(OnNativeDBConnection);
            BAGLDBConnectStringCommand = new DelegateCommand(OnBAGLDBConnectString);
            BAGLDBConnectionCommand = new DelegateCommand(OnBAGLDBConnection);
            DefaultCommand = new DelegateCommand(OnDefault);
            ApplyCommand = new DelegateCommand(OnApply);
            SaveCommand = new DelegateCommand(OnSave);
        }

        private void OnLoaded()
        {
            BasicConfigurationModel.OnLoaded();
        }

        private void OnOpenLogFileCatalogue()
        {
            BasicConfigurationModel.OnOpenLogFileCatalogue();
        }

        private void OnNativeDBConnection()
        {
            BasicConfigurationModel.NativeDBConnection();
        }

        private void OnBAGLDBConnectString()
        {
            BasicConfigurationModel.OnBAGLDBConnectString();
        }

        private void OnBAGLDBConnection()
        {
            BasicConfigurationModel.BAGLDBConnection();
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

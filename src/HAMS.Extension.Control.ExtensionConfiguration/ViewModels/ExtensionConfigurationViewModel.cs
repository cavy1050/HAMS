using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;
using HAMS.Extension.Control.ExtensionConfiguration.Models;

namespace HAMS.Extension.Control.ExtensionConfiguration.ViewModels
{
    public class ExtensionConfigurationViewModel : BindableBase
    {
        string extensionModuleName = "扩展设置";
        public string ExtensionModuleName
        {
            get => extensionModuleName;
            set => SetProperty(ref extensionModuleName, value);
        }

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand AdditionCommand { get; private set; }
        public DelegateCommand AlterationCommand { get; private set; }
        public DelegateCommand DeletionCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        ExtensionConfigurationModel extensionConfigurationModel;
        public ExtensionConfigurationModel ExtensionConfigurationModel
        {
            get => extensionConfigurationModel;
            set => SetProperty(ref extensionConfigurationModel, value);
        }

        public ExtensionConfigurationViewModel(IContainerProvider containerProviderArg)
        {
            ExtensionConfigurationModel = new ExtensionConfigurationModel(containerProviderArg);

            LoadedCommand = new DelegateCommand(OnLoaded);
            AdditionCommand = new DelegateCommand(OnAddition);
            AlterationCommand = new DelegateCommand(OnAlteration);
            DeletionCommand = new DelegateCommand(OnDeletion);
            SaveCommand = new DelegateCommand(OnSave);
        }

        private void OnLoaded()
        {
            ExtensionConfigurationModel.Loaded();
        }

        private void OnAddition()
        {

        }

        private void OnAlteration()
        {

        }

        private void OnDeletion()
        {

        }

        private void OnSave()
        {

        }
    }
}

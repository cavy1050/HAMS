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
        ExtensionConfigurationModel extensionConfigurationModel;
        public ExtensionConfigurationModel ExtensionConfigurationModel
        {
            get => extensionConfigurationModel;
            set => SetProperty(ref extensionConfigurationModel, value);
        }
    }
}

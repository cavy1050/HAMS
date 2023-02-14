using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Frame.Control.MainLeftDrawer.Models
{
    public class MainLeftDrawerModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IEnvironmentMonitor environmentMonitor;
        IEventServiceController eventServiceController;

        string eventJsonSentence;

        ObservableCollection<ExtensionModuleItemKind> items;
        public ObservableCollection<ExtensionModuleItemKind> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public MainLeftDrawerModel(IContainerProvider containerProviderArgs)
        {
            Items = new ObservableCollection<ExtensionModuleItemKind>();

            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
        }

        public void Loaded()
        {
            RequestExtensionModuleItemData();
            
        }

        public void RequestExtensionModuleItemData()
        {
            eventJsonSentence = eventServiceController.Request(EventServicePart.ExtensionModuleInitializationService, FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule, new EmptyContentKind());
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }
    }
}

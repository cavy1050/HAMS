using System;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Models
{
    public class MainWindowModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

        ApplicationAlterationRequestContentKind applicationAlterationRequestContent;

        ISnackbarMessageQueue mainMessageQueue;
        public ISnackbarMessageQueue MainMessageQueue
        {
            get => mainMessageQueue;
            set => SetProperty(ref mainMessageQueue, value);
        }

        IRegionManager regionManager;
        public IRegionManager RegionManager
        {
            get => regionManager;
            set => SetProperty(ref regionManager, value);
        }

        double workAreaWidth;
        public double WorkAreaWidth
        {
            get => workAreaWidth;
            set => SetProperty(ref workAreaWidth, value);
        }

        double workAreaHeight;
        public double WorkAreaHeight
        {
            get => workAreaHeight;
            set => SetProperty(ref workAreaHeight, value);
        }

        bool isLeftDrawerOpen;
        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set => SetProperty(ref isLeftDrawerOpen, value);
        }

        public MainWindowModel(IContainerProvider containerProviderArg)
        {
            WorkAreaWidth = SystemParameters.WorkArea.Width;
            WorkAreaHeight = SystemParameters.WorkArea.Height;

            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            MainMessageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            RegionManager = containerProviderArg.Resolve<IRegionManager>();
            eventServiceController= containerProviderArg.Resolve<IEventServiceController>();

            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnApplicationAlterationRequestService, ThreadOption.PublisherThread, false, x => x.Contains("ApplicationAlterationService"));
        }

        private void OnApplicationAlterationRequestService(string serviceTextArg)
        {
            MessageBox.Show(serviceTextArg);
        }
    }
}

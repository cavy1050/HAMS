using System;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Models
{
    public class MainWindowModel : BindableBase
    {
        IContainerProvider containerProvider;
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

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

            containerProvider = containerProviderArg;
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            MainMessageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            RegionManager = containerProviderArg.Resolve<IRegionManager>();

            eventAggregator.GetEvent<RequestServiceEvent>().Subscribe(OnRequestEventInitializationService, ThreadOption.PublisherThread, false, x => x.Contains("AccountVerificationService"));
        }

        public void WindowLoaded()
        {
            
        }

        private void OnRequestEventInitializationService(string requestServiceTextArg)
        {
            MessageBox.Show("23");
        }
    }
}

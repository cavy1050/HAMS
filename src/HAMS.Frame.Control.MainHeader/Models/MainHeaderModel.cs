using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Control.MainHeader.Models
{
    public class MainHeaderModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IEnvironmentMonitor environmentMonitor;

        bool isLeftDrawerOpen;
        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set => SetProperty(ref isLeftDrawerOpen, value);
        }

        string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public MainHeaderModel(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            environmentMonitor = containerProviderArgs.Resolve<IEnvironmentMonitor>();
        }

        public void Loaded()
        {
            UserName = environmentMonitor.UserSetting.Name;
        }
    }
}

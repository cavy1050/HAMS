using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;

namespace HAMS.Frame.Control.MainHeader.Models
{
    public class MainHeaderModel : BindableBase
    {
        IEventAggregator eventAggregator;

        bool isLeftDrawerOpen;
        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set => SetProperty(ref isLeftDrawerOpen, value);
        }

        public MainHeaderModel(IContainerProvider containerProviderArgs)
        {
            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
        }
    }
}

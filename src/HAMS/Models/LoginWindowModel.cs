using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Events;
using HAMS.Views;

namespace HAMS.Models
{
    public class LoginWindowModel : BindableBase
    {
        IEventAggregator eventAggregator;

        public LoginWindowModel(IContainerProvider containerProviderArg)
        {
            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
        }
    }
}

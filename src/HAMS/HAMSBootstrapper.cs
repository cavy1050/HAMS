using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using MaterialDesignThemes.Wpf;
using HAMS.Views;

namespace HAMS
{
    public class HAMSBootstrapper:PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistryArgs)
        {
            containerRegistryArgs.RegisterSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();
        }

    }
}

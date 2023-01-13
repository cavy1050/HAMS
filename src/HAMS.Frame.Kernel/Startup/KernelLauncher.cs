using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using FluentValidation.Results;
using System.Windows;

namespace HAMS.Frame.Kernel
{
    public class KernelLauncher
    {
        IContainerProvider containerProvider;

        IEnvironmentMonitor environmentMonitor;
        IManager<PathPart> pathManager;

        public KernelLauncher(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;

            environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize()
        {
            InitializeWithValidateServices();
        }

        private void InitializeWithValidateServices()
        {
            pathManager = containerProvider.Resolve<IManager<PathPart>>();
            pathManager.Initialize(PathPart.All);

            PathValidator pathValidator = new PathValidator();
            ValidationResult result = pathValidator.Validate(pathManager as PathManager);
        }
    }
}

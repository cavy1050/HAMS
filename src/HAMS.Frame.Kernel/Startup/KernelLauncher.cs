using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;
using FluentValidation.Results;

namespace HAMS.Frame.Kernel
{
    public class KernelLauncher
    {
        IContainerProvider containerProvider;

        IEnvironmentMonitor environmentMonitor;
        IManager<PathPart> pathManager;
        IManager<DataBasePart> dataBaseManager;

        public KernelLauncher(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;

            environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize()
        {
            DefaultInitializeWithValidateServices();
            InitializeWithValidateServices();
        }

        private void DefaultInitializeWithValidateServices()
        {
            pathManager = containerProvider.Resolve<IManager<PathPart>>();
            pathManager.DeInit(PathPart.All);

            DefaultPathValidator defaultPathValidator = new DefaultPathValidator();
            ValidationResult defaultPathResult = defaultPathValidator.Validate(pathManager as PathManager);
            if (defaultPathResult.IsValid != true)
                environmentMonitor.ValidationSetting.Errors.AddRange(defaultPathResult.Errors);
            else
            {
                pathManager.Load(PathPart.All);

                dataBaseManager = containerProvider.Resolve<IManager<DataBasePart>>();
                dataBaseManager.DeInit(DataBasePart.Native);

                DefaultDataBaseValidator defaultDataBaseValidator = new DefaultDataBaseValidator();
                ValidationResult defaultDataBaseResult = defaultDataBaseValidator.Validate(dataBaseManager as DataBaseManager);
                if (defaultDataBaseResult.IsValid != true)
                    environmentMonitor.ValidationSetting.Errors.AddRange(defaultDataBaseResult.Errors);
                else
                    dataBaseManager.Load(DataBasePart.All);
            }
        }

        private void InitializeWithValidateServices()
        {
            pathManager.Init(PathPart.All);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using log4net.Core;
using log4net.Repository;
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
        IManager<DataBasePart> dataBaseManager;
        IManager<LogPart> logManager;

        ILogController errorLogController;
        ILogController databaseLogController;
        ILogController serviceEventLogController;



        public KernelLauncher(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;

            environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();
        }

        public void Initialize()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
            InitializeWithValidateDefaultServices();
            InitializeWithValidateCustomServices();
            SaveDefaultServices();
        }

        private void InitializeWithValidateDefaultServices()
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
                {
                    dataBaseManager.Load(DataBasePart.Native);

                    logManager= containerProvider.Resolve<IManager<LogPart>>();
                    logManager.DeInit(LogPart.All);
                    logManager.Load(LogPart.All);
                }
            }
        }

        private void InitializeWithValidateCustomServices()
        {
            pathManager.Init(PathPart.All);
            CostomPathValidator costomPathValidator = new CostomPathValidator(containerProvider);
            ValidationResult costomPathResult = costomPathValidator.Validate(pathManager as PathManager);
            if (costomPathResult.IsValid != true)
                environmentMonitor.ValidationSetting.Errors.AddRange(costomPathResult.Errors);
            else
            {
                pathManager.Load(PathPart.All);
                dataBaseManager.Init(DataBasePart.All);

                CostomDataBaseValidator costomDataBaseValidator = new CostomDataBaseValidator();
                ValidationResult costomDataBaseResult = costomDataBaseValidator.Validate(dataBaseManager as DataBaseManager);
                if (costomDataBaseResult.IsValid != true)
                    environmentMonitor.ValidationSetting.Errors.AddRange(costomDataBaseResult.Errors);
                else
                {
                    dataBaseManager.Load(DataBasePart.All);

                    logManager.Init(LogPart.All);
                    logManager.Load(LogPart.All);
                }
            }
        }

        private void SaveDefaultServices()
        {
            errorLogController = environmentMonitor.LogSetting.GetContent(LogPart.Error);
            errorLogController.Write("hello1122");

            databaseLogController = environmentMonitor.LogSetting.GetContent(LogPart.DataBase);
            databaseLogController.Write("hello1122");

            serviceEventLogController = environmentMonitor.LogSetting.GetContent(LogPart.ServicEvent);
            serviceEventLogController.Write("hello1122");
        }
    }
}

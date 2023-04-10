using System;
using Prism.Ioc;
using Prism.Modularity;
using FluentValidation;
using FluentValidation.Results;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using HAMS.Frame.Kernel.Services;
using HAMS.Frame.Kernel.Extensions;

namespace HAMS.Frame.Kernel
{
    public class KernelLauncher
    {
        IContainerProvider containerProvider;
        IModuleManager moduleManager;
        IEnvironmentMonitor environmentMonitor;

        IManager<PathPart> pathManager;
        IManager<DataBasePart> dataBaseManager;
        IManager<LogPart> logManager;

        public KernelLauncher(IContainerProvider containerProviderArg)
        {
            containerProvider = containerProviderArg;
            moduleManager = containerProviderArg.Resolve<IModuleManager>();
            environmentMonitor = containerProvider.Resolve<IEnvironmentMonitor>();
        }

        public static void RegisterServices(IContainerRegistry containerRegistryArg)
        {
            containerRegistryArg.RegisterSingleton<IEnvironmentMonitor, EnvironmentMonitor>();

            containerRegistryArg.Register<IDataBaseController, NativeBaseController>(DataBasePart.Native.ToString());
            containerRegistryArg.Register<IDataBaseController, BAGLDBBaseController>(DataBasePart.BAGLDB.ToString());

            containerRegistryArg.Register<ICipherColltroller, CipherColltroller>();

            containerRegistryArg.Register<ILogController, ApplicationLogController>(LogPart.Application.ToString());
            containerRegistryArg.Register<ILogController, DataBaseLogController>(LogPart.DataBase.ToString());
            containerRegistryArg.Register<ILogController, ServiceEventLogController>(LogPart.ServicEvent.ToString());

            containerRegistryArg.Register<IManager<PathPart>, PathManager>();
            containerRegistryArg.Register<IManager<DataBasePart>, DataBaseManager>();
            containerRegistryArg.Register<IManager<LogPart>, LogManager>();

            containerRegistryArg.Register<IEventServiceController, EventServiceController>();
        }

        public void Initialize()
        {
            InitializeWithValidateDefaultServices();
            InitializeWithValidateCustomServices();
            SaveDefaultServices();
            LoadFrameModuleCatalog();
        }

        private void InitializeWithValidateDefaultServices()
        {
            pathManager = containerProvider.Resolve<IManager<PathPart>>();
            pathManager.DeInit(PathPart.All);

            PathValidator pathValidator = new PathValidator();
            ValidationResult pathResult = pathValidator.Validate(pathManager as PathManager);

            if (pathResult.IsValid != true)
                environmentMonitor.ValidationResult.Errors.AddRange(pathResult.Errors);
            else
            {
                pathManager.Load(PathPart.All);

                dataBaseManager = containerProvider.Resolve<IManager<DataBasePart>>();
                dataBaseManager.DeInit(DataBasePart.Native);

                DataBaseValidator dataBaseValidator = new DataBaseValidator();
                ValidationResult dataBaseResult = dataBaseValidator.Validate(dataBaseManager as DataBaseManager,options=>
                    {
                        options.IncludeRuleSets("NativeValidateRule");
                    });

                if (dataBaseResult.IsValid != true)
                    environmentMonitor.ValidationResult.Errors.AddRange(dataBaseResult.Errors);
                else
                {
                    dataBaseManager.Load(DataBasePart.Native);

                    logManager = containerProvider.Resolve<IManager<LogPart>>();
                    logManager.DeInit(LogPart.All);
                    LogValidator logValidator = new LogValidator();
                    ValidationResult logResult = logValidator.Validate(logManager as LogManager);
                    if (logResult.IsValid != true)
                        environmentMonitor.ValidationResult.Errors.AddRange(logResult.Errors);
                        
                    logManager.Load(LogPart.All);
                }
            }
        }

        private void InitializeWithValidateCustomServices()
        {
            pathManager.Init(PathPart.All);
            PathValidator pathValidator = new PathValidator();
            ValidationResult pathResult = pathValidator.Validate(pathManager as PathManager);
            if (pathResult.IsValid != true)
                environmentMonitor.ValidationResult.Errors.AddRange(pathResult.Errors);
            else
                pathManager.Load(PathPart.All);

            dataBaseManager.Init(DataBasePart.All);
            DataBaseValidator dataBaseValidator = new DataBaseValidator();
            ValidationResult dataBaseResult = dataBaseValidator.Validate(dataBaseManager as DataBaseManager,options=>
                {
                    options.IncludeRuleSets("BAGLDBValidateRule");
                });
            if (dataBaseResult.IsValid != true)
                environmentMonitor.ValidationResult.Errors.AddRange(dataBaseResult.Errors);
            else
                dataBaseManager.Load(DataBasePart.All);

            logManager.Init(LogPart.All);
            LogValidator logValidator = new LogValidator();
            ValidationResult logResult = logValidator.Validate(logManager as LogManager);
            if (logResult.IsValid != true)
                environmentMonitor.ValidationResult.Errors.AddRange(logResult.Errors);
            else
                logManager.Load(LogPart.All);
        }

        /// <summary>
        /// 初始化完成只保存默认值项目，自定义项目不做改动
        /// </summary>
        private void SaveDefaultServices()
        {
            pathManager.Save(PathPart.ApplictionCatalogue);
            pathManager.Save(PathPart.NativeDataBaseFilePath);

            dataBaseManager.Save(DataBasePart.Native);
        }

        private void LoadFrameModuleCatalog()
        {
            FrameModuleCatalog frameModuleCatalog = new FrameModuleCatalog(containerProvider);
            frameModuleCatalog.Load();

            moduleManager.Run();
        }
    }
}

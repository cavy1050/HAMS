using System;
using System.Collections.Generic;
using Prism.Modularity;
using Prism.Ioc;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Extensions
{
    public class FrameModuleCatalog : ModuleCatalog
    {
        IDataBaseController nativeBaseController;
        IEnvironmentMonitor environmentMonitor;
        IModuleCatalog moduleCatalog;

        string sqlSentence;
        List<SettingKind> frameModuleCatalogHub;

        public FrameModuleCatalog(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
            nativeBaseController = environmentMonitor.DataBaseSetting.GetContent(DataBasePart.Native);
            moduleCatalog = containerProviderArg.Resolve<IModuleCatalog>();
        }

        protected override void InnerLoad()
        {
            sqlSentence = "SELECT Code,Item,Name,Content,Description,Note,Rank,DefaultFlag,EnabledFlag FROM System_FrameModuleSetting WHERE EnabledFlag=True AND DefaultFlag=False ORDER BY Rank";
            nativeBaseController.Query<SettingKind>(sqlSentence, out frameModuleCatalogHub);

            foreach (SettingKind frameModuleCatalog in frameModuleCatalogHub)
            {
                ModuleInfo moduleInfo = new ModuleInfo
                {
                    ModuleName = frameModuleCatalog.Item,
                    ModuleType = frameModuleCatalog.Description,
                    Ref = GetFileAbsoluteUri(frameModuleCatalog.Content),
                    InitializationMode = InitializationMode.WhenAvailable
                };

                moduleCatalog.AddModule(moduleInfo);
            }
        }
    }
}

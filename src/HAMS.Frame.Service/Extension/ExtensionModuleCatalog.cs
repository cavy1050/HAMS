using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Service.Extension
{
    public class ExtensionModuleCatalog : ModuleCatalog
    {
        ModuleInfo moduleInfo;
        ExtensionModuleKind extensionModule;
        IModuleCatalog moduleCatalog;

        public ExtensionModuleCatalog(IContainerProvider containerProviderArg, ExtensionModuleKind extensionModuleArg)
        {
            extensionModule = extensionModuleArg;
            moduleCatalog = containerProviderArg.Resolve<IModuleCatalog>();
        }

        protected override void InnerLoad()
        {
            if (extensionModule != null)
            {
                if (string.IsNullOrEmpty(extensionModule.Note))
                    moduleInfo = new ModuleInfo
                    {
                        ModuleName = extensionModule.Item,
                        ModuleType = extensionModule.Description,
                        Ref = GetFileAbsoluteUri(extensionModule.Content),
                        InitializationMode = InitializationMode.WhenAvailable
                    };
                else
                    moduleInfo = new ModuleInfo
                    {
                        ModuleName = extensionModule.Item,
                        ModuleType = extensionModule.Description,
                        Ref = GetFileAbsoluteUri(extensionModule.Content),
                        DependsOn = new Collection<string>(extensionModule.Note.Split(',')),
                        InitializationMode = InitializationMode.WhenAvailable
                    };

                moduleCatalog.AddModule(moduleInfo);
            }
        }
    }
}

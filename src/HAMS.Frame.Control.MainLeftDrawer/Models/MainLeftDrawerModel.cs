using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;

namespace HAMS.Frame.Control.MainLeftDrawer.Models
{
    public class MainLeftDrawerModel : BindableBase
    {
        IEventAggregator eventAggregator;
        ISnackbarMessageQueue messageQueue;
        IEventServiceController eventServiceController;

        string eventJsonSentence;

        List<ExtensionModuleKind> extensionModuleHub;

        ObservableCollection<ModuleNodeKind> rootNodes;
        public ObservableCollection<ModuleNodeKind> RootNodes
        {
            get => rootNodes;
            set => SetProperty(ref rootNodes, value);
        }

        public MainLeftDrawerModel(IContainerProvider containerProviderArg)
        {
            RootNodes = new ObservableCollection<ModuleNodeKind>();

            eventAggregator = containerProviderArg.Resolve<IEventAggregator>();
            messageQueue = containerProviderArg.Resolve<ISnackbarMessageQueue>();
            eventServiceController = containerProviderArg.Resolve<IEventServiceController>();
        }

        public void Loaded()
        {
            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnResponseModuleInitializationService, ThreadOption.PublisherThread, false, x => x.Contains("ModuleInitializationService"));
            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnResponseModuleActivationService, ThreadOption.PublisherThread, false, x => x.Contains("ModuleActivationService"));
            RequestExtensionModuleNodeData();
        }

        private void RequestExtensionModuleNodeData()
        {
            eventJsonSentence = eventServiceController.Request(EventServicePart.ModuleInitializationService, FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule, new EmptyContentKind());
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }

        private void OnResponseModuleInitializationService(string responseServiceTextArg)
        {
            JObject responseObj = JObject.Parse(responseServiceTextArg);
            JObject responseContentObj = responseObj.Value<JObject>("svc_cont");
            JArray extensionModules = responseContentObj.Value<JArray>("menus");

            extensionModuleHub = JsonConvert.DeserializeObject<List<ExtensionModuleKind>>(extensionModules.ToString());
            LoadRootModuleData(extensionModuleHub);
        }

        private void LoadRootModuleData(List<ExtensionModuleKind> moduleArgs)
        {
            if (moduleArgs != null)
            {
                List<ExtensionModuleKind> rootModules = moduleArgs.FindAll(x => x.SuperCode == "");

                rootModules.ForEach(module => RootNodes.Add(new ModuleNodeKind
                {
                    Code = module.Code,
                    Name = module.Name,
                    ModuleName = module.Item,
                    ModuleRef = module.Content,
                    ModuleType = module.Description,
                    NextNodes = LoadNextModuleCollection(module.Code)
                }));
            }
        }

        private ObservableCollection<ModuleNodeKind> LoadNextModuleCollection(string superCodeArg)
        {
            ObservableCollection<ModuleNodeKind> retModuleNodes = new ObservableCollection<ModuleNodeKind>();

            if (superCodeArg != string.Empty)
            {
                List<ExtensionModuleKind> nextModules = extensionModuleHub.FindAll(x => x.SuperCode == superCodeArg);

                foreach (ExtensionModuleKind module in nextModules)
                {
                    ModuleNodeKind moduleNode = new ModuleNodeKind();
                    moduleNode.Code = module.Code;
                    moduleNode.Name = module.Name;
                    moduleNode.ModuleName = module.Item;
                    moduleNode.ModuleRef = module.Content;
                    moduleNode.ModuleType = module.Description;
                    moduleNode.NextNodes = LoadNextModuleCollection(module.Code);
                    moduleNode.NodeSelected += NextModuleNodeSelected;

                    retModuleNodes.Add(moduleNode);
                }
            }

            return retModuleNodes;
        }

        private void NextModuleNodeSelected(object sender, NodeSelectedEventArgs noteArg)
        {
            eventJsonSentence = eventServiceController.Request(EventServicePart.ModuleActivationService, FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule,
                new ModuleActivationRequestContentKind
                {
                    Code = noteArg.Code,
                    Name = noteArg.Name,
                    ModuleName = noteArg.ModuleName,
                    ModuleRef = noteArg.ModuleRef,
                    ModuleType = noteArg.ModuleType,
                });

            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }

        private void OnResponseModuleActivationService(string requestServiceTextArg)
        {
            JObject responseObj = JObject.Parse(requestServiceTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mod_name"));

            if (targetModule == FrameModulePart.MainLeftDrawerModule)
            {
                if (!responseObj.Value<bool>("ret_rst"))
                    messageQueue.Enqueue("启动程序扩展模块错误,详细信息请参阅日志!");
            }
        }
    }
}

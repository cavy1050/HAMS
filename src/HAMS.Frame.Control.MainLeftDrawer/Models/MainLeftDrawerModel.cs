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
        IEventController eventController;

        string eventJsonSentence;

        List<ExtensionModuleKind> extensionModuleHub;

        JObject responseObj, responseContentObj;

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
            eventController = containerProviderArg.Resolve<IEventController>();
        }

        public void Loaded()
        {
            eventAggregator.GetEvent<ResponseEvent>().Subscribe(OnExtensionModuleInitializationResponseEvent, ThreadOption.PublisherThread, false, x => x.Contains("ExtensionModuleEvent"));
            eventAggregator.GetEvent<ResponseEvent>().Subscribe(OnExtensionModuleActivationResponseEvent, ThreadOption.PublisherThread, false, x => x.Contains("ExtensionModuleEvent"));
            RequestExtensionModuleNodeData();
        }

        private void RequestExtensionModuleNodeData()
        {
            eventJsonSentence = eventController.Request(EventPart.ExtensionModuleEvent, EventBehaviourPart.Initialization , FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule, new EmptyContentKind());
            eventAggregator.GetEvent<RequestEvent>().Publish(eventJsonSentence);
        }

        private void OnExtensionModuleInitializationResponseEvent(string responseEventTextArg)
        {
            responseObj = JObject.Parse(responseEventTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mdl"));
            EventBehaviourPart eventBehaviour = (EventBehaviourPart)Enum.Parse(typeof(EventBehaviourPart), responseObj.Value<string>("svc_bhvr_type"));

            if (targetModule == FrameModulePart.MainLeftDrawerModule && eventBehaviour == EventBehaviourPart.Initialization)
            {
                responseContentObj = responseObj.Value<JObject>("svc_cont");

                JArray extensionModules = responseContentObj.Value<JArray>("menus");
                extensionModuleHub = JsonConvert.DeserializeObject<List<ExtensionModuleKind>>(extensionModules.ToString());
                LoadRootModuleData(extensionModuleHub);
            }
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
                    ModuleDependency = module.Note,
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
                    moduleNode.ModuleDependency= module.Note;
                    moduleNode.NextNodes = LoadNextModuleCollection(module.Code);
                    moduleNode.NodeSelected += NextModuleNodeSelected;

                    retModuleNodes.Add(moduleNode);
                }
            }

            return retModuleNodes;
        }

        private void NextModuleNodeSelected(object sender, NodeSelectedEventArgs noteArg)
        {
            eventJsonSentence = eventController.Request(EventPart.ExtensionModuleEvent, EventBehaviourPart.Activation, FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule,
                new ExtensionModuleActivationRequestContentKind
                {
                    Code = noteArg.Code,
                    Name = noteArg.Name,
                    ModuleName = noteArg.ModuleName,
                    ModuleRef = noteArg.ModuleRef,
                    ModuleType = noteArg.ModuleType,
                    ModuleDependency = noteArg.ModuleDependency
                });

            eventAggregator.GetEvent<RequestEvent>().Publish(eventJsonSentence);
        }

        private void OnExtensionModuleActivationResponseEvent(string responseEventTextArg)
        {
            responseObj = JObject.Parse(responseEventTextArg);
            FrameModulePart targetModule = (FrameModulePart)Enum.Parse(typeof(FrameModulePart), responseObj.Value<string>("tagt_mdl"));
            EventBehaviourPart eventBehaviour = (EventBehaviourPart)Enum.Parse(typeof(EventBehaviourPart), responseObj.Value<string>("svc_bhvr_type"));

            if (targetModule == FrameModulePart.MainLeftDrawerModule && eventBehaviour == EventBehaviourPart.Activation)
            {
                if (!responseObj.Value<bool>("ret_rst"))
                    messageQueue.Enqueue("启动程序扩展模块错误,详细信息请参阅日志!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Events;
using System.Windows;

namespace HAMS.Frame.Control.MainLeftDrawer.Models
{
    public class MainLeftDrawerModel : BindableBase
    {
        IEventAggregator eventAggregator;
        IEventServiceController eventServiceController;

        string eventJsonSentence;

        List<ExtensionModuleKind> extensionModuleHub;

        ObservableCollection<ModuleNodeKind> items;
        public ObservableCollection<ModuleNodeKind> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public MainLeftDrawerModel(IContainerProvider containerProviderArgs)
        {
            Items = new ObservableCollection<ModuleNodeKind>();

            eventAggregator = containerProviderArgs.Resolve<IEventAggregator>();
            eventServiceController = containerProviderArgs.Resolve<IEventServiceController>();
        }

        public void Loaded()
        {      
            eventAggregator.GetEvent<ResponseServiceEvent>().Subscribe(OnResponseExtensionModuleInitializationServiceService, ThreadOption.PublisherThread, false, x => x.Contains("ExtensionModuleInitializationService"));
            RequestExtensionModuleItemData();
        }

        private void RequestExtensionModuleItemData()
        {
            eventJsonSentence = eventServiceController.Request(EventServicePart.ExtensionModuleInitializationService, FrameModulePart.MainLeftDrawerModule, FrameModulePart.ServiceModule, new EmptyContentKind());
            eventAggregator.GetEvent<RequestServiceEvent>().Publish(eventJsonSentence);
        }

        private void OnResponseExtensionModuleInitializationServiceService(string responseServiceTextArg)
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

                rootModules.ForEach(module => Items.Add(new ModuleNodeKind
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
                    ModuleNodeKind moduleNote = new ModuleNodeKind();
                    moduleNote.Code = module.Code;
                    moduleNote.Name = module.Name;
                    moduleNote.ModuleName = module.Item;
                    moduleNote.ModuleRef = module.Content;
                    moduleNote.ModuleType = module.Description;
                    moduleNote.NextNodes = LoadNextModuleCollection(module.Code);
                    moduleNote.NodeSelected += ModuleNoteSelected;

                    retModuleNodes.Add(moduleNote);
                }
            }

            return retModuleNodes;
        }

        private void ModuleNoteSelected(object sender, NodeSelectedEventArgs e)
        {
            MessageBox.Show(e.Code + "|" + e.Name + "|" + e.ModuleName + "|" + e.ModuleRef + "|" + e.ModuleType);
        }
    }
}

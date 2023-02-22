using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Commands;

namespace HAMS.Frame.Control.MainLeftDrawer.Models
{
    public class ModuleNodeKind : BindableBase
    {
        string code;
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }

        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        string moduleName;
        public string ModuleName
        {
            get => moduleName;
            set => SetProperty(ref moduleName, value);
        }

        string moduleRef;
        public string ModuleRef
        {
            get => moduleRef;
            set => SetProperty(ref moduleRef, value);
        }

        string moduletype;
        public string ModuleType
        {
            get => moduletype;
            set => SetProperty(ref moduletype, value);
        }

        bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);

                if (isSelected)
                    OnNodeSelected(Code, Name, ModuleName, ModuleRef, ModuleType);
            }
        }

        ObservableCollection<ModuleNodeKind> nextNodes;
        public ObservableCollection<ModuleNodeKind> NextNodes
        {
            get => nextNodes;
            set => SetProperty(ref nextNodes, value);
        }

        public DelegateCommand NextNodeSwitchCommand { get; private set; }

        public event EventHandler<NodeSelectedEventArgs> NodeSelected;

        public void OnNodeSelected(string codeArg, string nameArg, string moduleNameArg, string moduleRefArg, string moduleTypeArg)
        {
            NodeSelected?.Invoke(this, new NodeSelectedEventArgs
            {
                Code = codeArg,
                Name = nameArg,
                ModuleName = moduleNameArg,
                ModuleRef = moduleRefArg,
                ModuleType = moduleTypeArg
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using HAMS.Frame.Kernel.Core;

namespace HAMS.Frame.Control.MainLeftDrawer.Models
{
    public class ExtensionModuleItemKind : BindableBase
    {
        string itemCode;
        public string ItemCode
        {
            get => itemCode;
            set => SetProperty(ref itemCode, value);
        }

        string itemName;
        public string ItemName
        {
            get => itemName;
            set => SetProperty(ref itemName, value);
        }

        string superCode;
        public string SuperCode
        {
            get => superCode;
            set => SetProperty(ref superCode, value);
        }

        bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set => SetProperty(ref isExpanded, value);
        }

        bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);

                if (isSelected)
                    OnItemSelected(this.ItemCode);
            }
        }

        ObservableCollection<ExtensionModuleItemKind> nextItems;
        public ObservableCollection<ExtensionModuleItemKind> NextItems
        {
            get => nextItems;
            set => SetProperty(ref nextItems, value);
        }

        public event EventHandler<ItemSelectedEventArgs> ItemSelected;

        public void OnItemSelected(string itemCodeArg)
        {
            if (ItemSelected != null)
            {
                ItemSelected(this, new ItemSelectedEventArgs
                {
                    ItemCode = itemCodeArg
                });
            }
        }
    }
}

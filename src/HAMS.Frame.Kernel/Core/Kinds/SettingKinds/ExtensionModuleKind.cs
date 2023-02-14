using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAMS.Frame.Kernel.Core
{
    public class ExtensionModuleKind : NestKind
    {
        [JsonProperty(PropertyName = "menu_code", Order = 1)]
        public override string Code 
        { 
            get => base.Code; 
            set => base.Code = value; 
        }

        [JsonProperty(PropertyName = "menu_name", Order = 2)]
        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        [JsonProperty(PropertyName = "menu_super_code", Order = 3)]
        public override string SuperCode
        {
            get => base.SuperCode;
            set => base.SuperCode = value;
        }

        [JsonProperty(PropertyName = "menu_super_item", Order = 4)]
        public override string SupeItem
        {
            get => base.SupeItem;
            set => base.SupeItem = value;
        }

        [JsonProperty(PropertyName = "menu_mod_name", Order = 5)]
        public override string Item
        {
            get => base.Item;
            set => base.Item = value;
        }

        [JsonProperty(PropertyName = "menu_mod_ref", Order = 6)]
        public override string Content
        {
            get => base.Content;
            set => base.Content = value;
        }

        [JsonProperty(PropertyName = "menu_mod_type", Order = 7)]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }

        [JsonProperty(PropertyName = "menu_rank", Order = 8)]
        public override int Rank
        {
            get => base.Rank;
            set => base.Rank = value;
        }
    }
}

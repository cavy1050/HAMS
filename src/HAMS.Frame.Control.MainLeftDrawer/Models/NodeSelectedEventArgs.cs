using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Control.MainLeftDrawer.Models
{
    public class NodeSelectedEventArgs : EventArgs
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ModuleName { get; set; }
        public string ModuleRef { get; set; }
        public string ModuleType { get; set; }
    }
}

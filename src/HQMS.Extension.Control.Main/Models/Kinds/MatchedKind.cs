using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Extension.Control.Main.Models
{
    public class MatchedKind : CatalogKind
    {
        public string LocalCode { get; set; }
        public string LocalName { get; set; }
        public string StandardCode { get; set; }
        public string StandardName { get; set; }
    }
}

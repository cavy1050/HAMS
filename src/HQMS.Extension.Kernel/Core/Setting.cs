using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Extension.Kernel.Core
{
    public class Setting : ISetting
    {
        public string HospitalCode { get; set; }
        public string UpLoadFileCatalogue { get; set; }
        public DisplayModePart DisplayMode { get; set; }
    }
}

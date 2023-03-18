using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Extension.Control.Main
{
    public class Configurator : IConfigurator
    {
        public string HospitalCode { get; set; }
        public string ExportFileCatalogue { get; set; }
        public string UpLoadFileCatalogue { get; set; }
        public string MasterExportFileCatalogue { get; set; }
        public string DetailExportFileCatalogue { get; set; }
    }
}

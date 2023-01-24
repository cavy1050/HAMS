using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace HAMS.Frame.Kernel.Core
{
    public class EnvironmentMonitor : IEnvironmentMonitor
    {
        public SeverityCollector SeveritySetting { get; set; }
        public PathCollector PathSetting { get; set; }
        public DataBaseCollector DataBaseSetting { get; set; }
        public LogCollector LogSetting { get; set; }

        public EnvironmentMonitor()
        {
            SeveritySetting = new SeverityCollector();
            PathSetting = new PathCollector();
            DataBaseSetting = new DataBaseCollector();
            LogSetting = new LogCollector();
        }
    }
}

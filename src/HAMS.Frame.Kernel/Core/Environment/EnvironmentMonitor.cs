using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public class EnvironmentMonitor : IEnvironmentMonitor
    {
        public PathCollecter PathSetting { get; set; }

        public EnvironmentMonitor()
        {
            PathSetting = new PathCollecter();
        }
    }
}

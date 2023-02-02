using System;
using System.Collections.Generic;

namespace HAMS.Frame.Kernel.Core
{
    public class EnvironmentMonitor : IEnvironmentMonitor
    {
        public Dictionary<ControlTypePart, ActiveFlagPart> ApplicationControlSetting { get; set; }
        public SeverityCollector SeveritySetting { get; set; }
        public Queue<string> MessageSetting { get; set; }

        public PathCollector PathSetting { get; set; }
        public DataBaseCollector DataBaseSetting { get; set; }
        public LogCollector LogSetting { get; set; }

        public UserKind UserSetting { get; set; }

        public EnvironmentMonitor()
        {
            ApplicationControlSetting = new Dictionary<ControlTypePart, ActiveFlagPart>();
            SeveritySetting = new SeverityCollector();
            MessageSetting = new Queue<string>();

            PathSetting = new PathCollector();
            DataBaseSetting = new DataBaseCollector();
            LogSetting = new LogCollector();

            UserSetting = new UserKind();
        }
    }
}

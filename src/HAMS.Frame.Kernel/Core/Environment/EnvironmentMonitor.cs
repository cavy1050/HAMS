using System;
using System.Collections;
using System.Collections.Generic;
using FluentValidation.Results;

namespace HAMS.Frame.Kernel.Core
{
    public class EnvironmentMonitor : IEnvironmentMonitor
    {
        public Dictionary<ControlTypePart, ActiveFlagPart> ApplicationControlSetting { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public PathCollector PathSetting { get; set; }
        public DataBaseCollector DataBaseSetting { get; set; }
        public LogCollector LogSetting { get; set; }

        public SettingKind UserSetting { get; set; }

        public EnvironmentMonitor()
        {
            ApplicationControlSetting = new Dictionary<ControlTypePart, ActiveFlagPart>();
            ValidationResult = new ValidationResult();

            PathSetting = new PathCollector();
            DataBaseSetting = new DataBaseCollector();
            LogSetting = new LogCollector();

            UserSetting = new SettingKind();
        }
    }
}

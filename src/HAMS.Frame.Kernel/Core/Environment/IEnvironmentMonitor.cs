﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace HAMS.Frame.Kernel.Core
{
    /// <summary>
    /// 全局单例环境存储器,该类型提供程序路径信息、数据库信息、验证信息的外部访问支持.
    /// </summary>
    public interface IEnvironmentMonitor
    {
        Dictionary<ControlTypePart, ActiveFlagPart> ApplicationControlSetting { get; set; }
        SeverityCollector SeveritySetting { get; set; }
        Queue<string> MessageSetting { get; set; }


        PathCollector PathSetting { get; set; }
        DataBaseCollector DataBaseSetting { get; set; }
        LogCollector LogSetting { get; set; }
        SettingKind UserSetting { get; set; }
    }
}

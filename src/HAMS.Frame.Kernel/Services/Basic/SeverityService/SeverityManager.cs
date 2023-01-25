using System;
using Prism.Ioc;
using FluentValidation.Results;
using HAMS.Frame.Kernel.Core;
using HAMS.Frame.Kernel.Extensions;

namespace HAMS.Frame.Kernel.Services
{
    public class SeverityManager : IManager<SeverityLevelPart>
    {
        IEnvironmentMonitor environmentMonitor;

        public SeverityManager(IContainerProvider containerProviderArg)
        {
            environmentMonitor = containerProviderArg.Resolve<IEnvironmentMonitor>();
        }

        public void DeInit(SeverityLevelPart severityLevelParttArg)
        {

        }

        public void Init(SeverityLevelPart severityLevelParttArg)
        {

        }

        public void Load(SeverityLevelPart severityLevelParttArg)
        {
            switch (severityLevelParttArg)
            {
                case SeverityLevelPart.Info:
                    if (!environmentMonitor.SeveritySetting.Exists(x => x.Code == "01GQCT5JMSEWPN94XQHF0SWCRH"))
                        environmentMonitor.SeveritySetting.Add(new SeverityKind
                        {
                            Code = "01GQCT5JMSEWPN94XQHF0SWCRH",
                            Item = SeverityLevelPart.Info.ToString(),
                            Name = EnumExtension.GetDescription(SeverityLevelPart.Info),
                            Rank = Convert.ToInt32(SeverityLevelPart.Info),
                            EnabledFlag = true
                        });

                    environmentMonitor.SeveritySetting[SeverityLevelPart.Info].Results = new ValidationResult();
                    break;

                case SeverityLevelPart.Error:
                    if (!environmentMonitor.SeveritySetting.Exists(x => x.Code == "01GQCT5JMSH8Z6R8ES8H9DSJ52"))
                        environmentMonitor.SeveritySetting.Add(new SeverityKind
                        {
                            Code = "01GQCT5JMSH8Z6R8ES8H9DSJ52",
                            Item = SeverityLevelPart.Error.ToString(),
                            Name = EnumExtension.GetDescription(SeverityLevelPart.Error),
                            Rank = Convert.ToInt32(SeverityLevelPart.Error),
                            EnabledFlag = true
                        });

                    environmentMonitor.SeveritySetting[SeverityLevelPart.Error].Results = new ValidationResult();
                    break;

                case SeverityLevelPart.All:
                    Load(SeverityLevelPart.Info);
                    Load(SeverityLevelPart.Error);
                    break;
            }
        }

        public void Save(SeverityLevelPart severityLevelParttArg)
        {

        }
    }
}
